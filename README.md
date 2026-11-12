# typescript-go-cs

TypeScript's Go port ([microsoft/typescript](https://github.com/microsoft/typescript)) converted to C# by [go2cs](https://github.com/ritchiecarroll/go2cs) and run against the native Go build's own outputs. This port is currently pinned at Go version 1.26 until go2cs migrates to 1.27.

> [!IMPORTANT]
> **This is a work in progress, and a proof of what go2cs can do, i.e., produce operational .NET code transpiled from Go source. The code in this repository is \*\*_not_\*\* intended to replace the official Go TypeScript compiler.** To compile
> TypeScript, use the official [TypeScript](https://www.typescriptlang.org/) toolchain. This repository is not
> affiliated with or endorsed by Microsoft or the TypeScript team. TypeScript is a trademark of Microsoft; its name is
> used here only to say what was converted.

## Status

The numbers below are measured, not projected. They come from a pre-release go2cs build (go2cs master `06610aaa65`) on
linux/amd64. Every comparison is byte-for-byte against the native Go build of the same TypeScript source.

| What runs, converted to C# | Result |
|---|---|
| Parse: a corpus of 17,422 source files drawn from TypeScript's own tests | **17,422 of 17,422** identical to native |
| Type-check: TypeScript's compiler test units, one at a time | **7,404 of 7,404** identical |
| Emit: the same compiler test units | **7,404 of 7,404** identical |
| The parse, type-check and emit demos | identical |
| Two multi-file `tsconfig` projects (diagnostics and every output file) | identical |

How it was measured:

- **Source:** TypeScript at commit
  [`ba29a562991ceb942481e2a00810b4e5e4062e52`](https://github.com/microsoft/typescript-go/commit/ba29a562991ceb942481e2a00810b4e5e4062e52)
  (Go module `github.com/microsoft/TypeScript/tsc`).
- **Input files:** inputs use LF line endings.
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
  and none of them is part of the parse, type-check or emit path measured above.

Performance parity is not a goal of go2cs, nor of this proof, see [FAQ](https://go2cs.net/FAQ.html#is-converted-code-slower-than-go). go2cs strives to bring Go code to .NET with visual and behavioral parity first, performance optimizations are currently secondary. You should expect converted TypeScript Go code running in .NET to be slower.

## Licenses

- TypeScript is licensed under the Apache License 2.0. When the converted code arrives, it brings TypeScript's
  `LICENSE.txt` and `NOTICE.txt` verbatim, the notices of each third-party Go module it includes, and a record of every
  change made to the source.
- go2cs, the converter, is licensed separately; see its repository.

---

![go2cs](https://go2cs.net/images/go2cs-small.png)

This project was converted by [go2cs](https://github.com/ritchiecarroll/go2cs) — the official Go to C# Converter.