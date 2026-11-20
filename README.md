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
