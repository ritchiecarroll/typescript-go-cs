# typescript-go-cs

[![converted by go2cs][go2cs-badge]](https://go2cs.net)

TypeScript's native Go port (the `tsc` module of [microsoft/TypeScript](https://github.com/microsoft/TypeScript))
converted to C# by [go2cs](https://github.com/ritchiecarroll/go2cs) and run against the native Go build's own outputs.
It is pinned to the last TypeScript commit before TypeScript moved to Go 1.27, and moves forward when go2cs does.

> [!IMPORTANT]
> **This is a work in progress, and a proof of what go2cs can do, i.e., produce operational .NET code transpiled from Go
> source. The code in this repository is _not_ intended to replace the official TypeScript compiler.** To compile
> TypeScript, use the official [TypeScript](https://www.typescriptlang.org/) toolchain. This repository is not
> affiliated with or endorsed by Microsoft or the TypeScript team. TypeScript is a trademark of Microsoft; its name is
> used here only to say what was converted.

## Status

The numbers below are measured, not projected. They come from the released go2cs 1.26.11.1 (go2cs commit `5dd5cafd5b`),
built against the go.* 1.26.11.1 packages on nuget.org and compared against go1.26.11, on linux/amd64 on 2026-11-20.
Every comparison is byte-for-byte against the native Go build of the same TypeScript source; [PROOF.md](PROOF.md) has
the method and both sides' outputs.

| What runs, converted to C# | Result |
|---|---|
| Parse: a corpus of 17,422 source files drawn from TypeScript's own tests | **17,422 of 17,422** identical to native |
| Type-check: TypeScript's compiler test units, one at a time | **7,404 of 7,404** identical |
| Emit: the same compiler test units (JavaScript, declaration files and emit diagnostics) | **7,404 of 7,404** identical |
| The parse, type-check and emit demos | identical |
| Two multi-file `tsconfig` projects (diagnostics and every output file) | identical |

How it was measured:

- **Source:** TypeScript at commit
  [`ba29a562991ceb942481e2a00810b4e5e4062e52`](https://github.com/microsoft/TypeScript/commit/ba29a562991ceb942481e2a00810b4e5e4062e52)
  (Go module `github.com/microsoft/TypeScript/tsc`, `go 1.26.0`).
- **Line endings:** every input file uses LF.
- **One change to TypeScript's `go.mod`:** `github.com/zeebo/xxh3` is replaced by a fork kept in the go2cs repository.
  The fork is xxh3 v1.1.0 with its `unsafe` memory views removed, and it computes the same hashes bit for bit. go2cs
  cannot yet convert those views; a planned go2cs change removes the need for the fork.
- **No hand edits:** none of the converted C# is edited by hand.

## What is not covered

- the language service and its LSP server
- `cmd/tsc`, the command-line compiler
- watch mode
- the five packages go2cs refuses to convert today: `internal/ls`, `internal/ls/lsconv`, `internal/ls/autoimport`,
  `internal/project` and `internal/fourslash`. Each one uses a generic constraint that go2cs cannot yet express in C#,
  and none of them is on the parse, type-check or emit path measured above.

Performance parity is not a goal of go2cs, nor of this proof (see the
[FAQ](https://go2cs.net/FAQ.html#is-converted-code-slower-than-go)). go2cs strives to bring Go code to .NET with visual
and behavioral parity first; performance optimizations are currently secondary. Expect the converted TypeScript code to
run slower in .NET than the native Go build.

The demo projects turn on server garbage collection with dynamic adaptation off. That makes type-check faster and uses
more memory; go2cs's [tuning guide](https://go2cs.net/Tuning.html) explains the setting.

<!-- typescript-go-cs:performance -->
## Performance

The converted compiler is slower than native Go. That is expected for a line-by-line translation running on a
different runtime, and it is improving. The table gives seconds. Each time is split into start-up (the time before any
TypeScript work begins) and work (everything after it), so the work columns can be compared directly.

<!-- Measured by G (go2cs fleet) 2026-11-20, COORD item 2. Source data kept on G-LAPTOP (ts.tsv, ts.tsv.sha256, the
     type-check trace summary). Method, as this section's draft states it: start-up = the median wall of the row's
     OWN binary on its minimal input (an empty .ts file; for mfdemo, a project holding only a tsconfig.json); work =
     the row's median wall minus that start-up; median of 5 timed runs after 1 warm-up, one process per run, after one
     verify run whose stdout SHA-256 equalled native's (11 of 11 cases). Harness: tsbench (the go2cs perf run's armab
     plus an argument column and CPU time). Inputs: the 17,422-unit parser corpus from go/cmd/parsecorpus-extract; the
     101-unit slice is every 74th of the 7,404 single-file compiler tests (the same ids as the go2cs repository's
     docs/phase4/data/ts-perf/units-101.txt). -->
Measured on G-LAPTOP (AMD Ryzen 5 PRO 6650U, 30.8 GB of RAM, Windows 11), 2026-11-20. The native column is Go 1.26.11
for windows/amd64; the converted columns are win-x64 on .NET 10 (SDK 10.0.400, runtime 10.0.11) against go.* 1.26.11.1.

Each cell reads start-up + work = total, in seconds.

| Case | What runs | Go | Converted, JIT |
|:--|:--|--:|--:|
| Parse | `parsedemo`, the 17,422-file parser corpus in one process | 0.03 + 2.76 = 2.79 | 1.52 + 18.46 = 19.98 |
| Type-check | `checkdemo`, 101 compiler-test files, each its own program, in one process | 0.07 + 3.09 = 3.16 | 3.68 + 47.71 = 51.39 |
| Emit | `emitdemo`, the same files, JavaScript and `.d.ts` | 0.07 + 2.91 = 2.98 | 3.82 + 45.52 = 49.34 |
| Multi-file | `mfdemo` on `shop` and `legacy` | 0.03 + 0.43 = 0.46 | 2.21 + 10.57 = 12.77 |

Every converted run printed exactly what the native build printed: each case's output was compared by SHA-256 before
any run was timed.

There is no Native AOT column yet. A Native AOT publish of `checkdemo` for win-x64 on this machine did not finish in
2.5 hours: the AOT compiler was still working, at a 13 GB peak, when it was stopped. It did not run out of memory.
<!-- The publish: dotnet publish checkdemo.csproj -c Release -r win-x64 -p:PublishAot=true
     -p:GoStdLibVersion=1.26.11.1; SDK 10.0.400, ILC 10.0.11; started 02:55, stopped at 05:30 local (9,295 s), ilc.exe
     at about 2.6 CPU-hours. One attempt, as ruled; not retried. -->

### Where the time goes

These are the largest differences we measured, and each is a target for future work in go2cs.

- **Start-up.** On win-x64, a converted demo spends 1.5 to 3.8 s before it does any work, where native Go on
  windows/amd64 needs 0.03 to 0.07 s: compiling methods on first call, running every package's initializers, and
  building Go-style method sets by reflection the first time a type is asserted to an interface. Native AOT removes
  the compiling; the rest remains.
- **Garbage collection.** The converted compiler allocates much more than the Go original: 15.4 GB over the 101-file
  type-check on win-x64. Collection pauses were about 15% of that type-check's time (7.9 s of 53 s in a traced run).
  The demo projects here turn on server garbage collection with dynamic adaptation off, which cut that type-check from
  90.8 s to 51.4 s on win-x64, at a cost in memory: a peak working set of 9.9 GB, against 1.0 GB with the default
  workstation collector. go2cs's [tuning guide](https://go2cs.net/Tuning.html)
  explains the setting.
- **Field views on embedded structs.** Each AST node reaches its embedded base through a cached field view, and those
  views were about two thirds of all bytes allocated in the win-x64 type-check (66%: the view entries 22% and the
  field boxes 44%), with element references another 9%.
- **Struct hash keys.** Maps keyed by structs hash through .NET's general value-type hash, where Go hashes the
  fields directly. (From an earlier profile on linux/amd64; not re-measured here.)
- **Range-over-func iterators** run on coroutines, which cost a thread hand-off where Go makes a direct call. (From
  the same earlier linux/amd64 profile.)

Fixed in go2cs since the first measurement: a string conversion that copied the rest of the source text for each
non-ASCII character (the largest single source of allocation), nil checks on named maps that went through
reflection, and struct equality that boxed both operands.
<!-- /typescript-go-cs:performance -->

## What is here

| Path | What it is |
|:--|:--|
| `src/github.com/microsoft/TypeScript/tsc/internal/…` | The 48 TypeScript packages the four demos need, converted by go2cs. Do not edit them: change the Go and convert again. |
| `src/github.com/microsoft/TypeScript/tsc/cmd/…` | The four demos, converted. Their Go sources are under `go/cmd/`. |
| `pkg/…` | The 12 third-party Go packages they import, converted, each module with its own license at its root. |
| `go/` | The Go sources this project added: the four demos and the corpus extractor. They are not part of TypeScript. |
| `samples/`, `proof/` | The demo inputs, the harness and its measured outputs. |
| `LICENSE`, `NOTICE.txt` | TypeScript's Apache-2.0 license and its notices, verbatim. |
| `MODIFICATIONS.md` | What was changed and what was added, as the license requires. |

## Building and running

Building needs the .NET 10 SDK. The restore takes the go2cs standard library (`go.*`) from nuget.org.

```
dotnet build src/github.com/microsoft/TypeScript/tsc/cmd/checkdemo/checkdemo.csproj -c Release -p:GoStdLibVersion=1.26.11.1
dotnet run --project src/github.com/microsoft/TypeScript/tsc/cmd/checkdemo/checkdemo.csproj -c Release -p:GoStdLibVersion=1.26.11.1 -- samples/check.ts
```

The same two commands build and run `parsedemo`, `emitdemo` (one or more `.ts` files) and `mfdemo` (one or more
project directories). [PROOF.md](PROOF.md) shows how to run each comparison.

## Provenance

- **Converted:** with `go2cs -recurse=nuget` at go2cs commit `5dd5cafd5b`.
- **Built:** against go.* `1.26.11.1` with the .NET SDK 10.0.400.
- **Compared:** against go1.26.11 building the same Go sources, unmodified.

## Licenses

- TypeScript is licensed under the Apache License 2.0. The converted code carries TypeScript's license
  ([LICENSE](LICENSE)) and [NOTICE.txt](NOTICE.txt) verbatim, each third-party Go module's license at its root under
  `pkg/`, and [MODIFICATIONS.md](MODIFICATIONS.md), the record of every change made to the source.
- go2cs, the converter, is licensed separately; see its repository.

[go2cs-badge]: https://cdn.jsdelivr.net/gh/ritchiecarroll/go2cs@master/docs/images/go2cs-badge.svg
