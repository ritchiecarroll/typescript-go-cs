# typescript-go-cs

[![converted by go2cs](https://img.shields.io/badge/converted%20by-go2cs-68217A)](https://go2cs.net)

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

The numbers below are measured, not projected. They come from a pre-release go2cs build (go2cs commit `06610aaa65`) on
linux/amd64. Every comparison is byte-for-byte against the native Go build of the same TypeScript source. The converted
code itself lands here once it is built against go2cs's published packages, and it is measured again then.

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

## Licenses

- TypeScript is licensed under the Apache License 2.0. When the converted code arrives, it brings TypeScript's
  `LICENSE.txt` and `NOTICE.txt` verbatim, the notices of each third-party Go module it includes, and a record of every
  change made to the source.
- go2cs, the converter, is licensed separately; see its repository.

---

<sub>Converted from Go to C# by [go2cs](https://github.com/ritchiecarroll/go2cs).</sub>
