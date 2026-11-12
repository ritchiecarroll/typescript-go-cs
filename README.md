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

[go2cs-badge]: https://img.shields.io/badge/converted%20by-go2cs-68217A?logo=data%3Aimage%2Fpng%3Bbase64%2CiVBORw0KGgoAAAANSUhEUgAAACYAAAAcCAYAAAAN3M1lAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAiBSURBVFhHtZd5UNNnGsdVXA9qK8iqRQ4VsB2tdvFmxeK4tba66pZZQEXUCkIOgglBkISbcCUBwyEQBYEIRBQFRDkHCKJQlCp4cV8iAnJZ2lql7f6%2BO%2FmpLPlxzNhxvzPff5LnfZ9Pnvd5j0yZ8o56EJFkSIjFH1A%2FHy0AH1%2BMyjkSzZKliG2k1cEW0U3Be6Prw%2B3iixO90wUVFdUbqGPeWb0BJ7VTEi%2BZJsTJv%2FI5nSp3K73za0Bu2cPMhAurqLEA5pwTpAcIdkf18taKwTESgKnvBbquJxi6nmAt8YXrZyHw3BKKKHpSdnn57dXUOSYVpkxRk0lT9gak51w5llPWz8krg3vedRws%2FB5rjnti%2F5UiRGQrwkePuX69ak2YTVy12yohaDp8OOi7g76UP8a0JTw46LiDbSCA71eSF%2BdDs9ij55lQaWExJoKs4grX6hbQ2wbhX9OItv4B%2FEYQUHQ%2Bw76S27DLLERCQhrz7RhF4c11gebR%2FU6LfScEoloJSFvEh9cmCWRB6QJVCoqkcalHGDk3Xn7X0IPDzf04crsOLR1PoFR%2BXh7ORkXCv%2FYxGM398E7NLFGO6f65e0GIZWy7o54PmYwK4KDPg4Pea4%2F7%2FSJ3eG2WIEOaa0PlISWLS93pVFCByPsNKK1vwrm6Fhy4eQ8NnV0kWIY8FQ50Gnh1T0BrGwS3phXnczKtpSxZHHdZEBwWj0q6xAP2OjwcM%2FADb50I7puE4G0UwWV5IGi6r6ulAqjjCcHuiP723nbtESBYWqrJZRmHHc9f7Yq%2BVw%2F88Tsw%2FAqt92sQWlMPbuVDlDR34Gb7U%2FjercfR2qdg1j0FrbUf%2FjllP%2FuuF%2F7O0vUlG11ptkEAaIau4GzwIk4xZdfSo3NtFLk3TLKSCvfE89Ki%2FHZIhlhLfVV%2BCG0xD64rQhDHl4tHwMIv5kbQskvwz4xCFDe3k9W5e%2BcOvjH7ApxIKaYu%2FRT8ME%2B43roD28Y%2BEupY6wC4Q%2F8BI16BbzX2wUxrB0w0t2K9phk2ztkOCZ2FB6XnHgOYOpLojWqba1eKrKV1rCU%2BoC8ZXTUP%2BO0O7xzE4Fwy0Du3rHNPdgn%2BlVWEgOoG9D57hheDA0govgG36DDIPa3QkiOBsFQBu8Y%2BsJp68d2NGqxlsDF3vg5eb2JV62jPhT%2BPq2xOTSqYUtUPHxp7bzv5UtlfZO%2Fp8kHX9oCfaRSyU4p2kEHhKRk%2B3PybsC27C%2FuGHjiV14BXeR9HHnQipiwHVdfTcf%2FWDwj4%2Fh7sm3rBePQEWp8sHwND9cxZ6nBz4cmpUG8VwTyb5r06AjwTETy3ivsDzU%2FdjaQlpWZIcw1Hgq4JJGZuGYU9zMfPQWvoIQEdG3tg%2B6gLjlUN4CkqYX%2B%2FHU4tfTha1YDZ87TGgIzntWvWobameaUK0RtdOZdvlOyfxS66WrGh7XmbMUEQetQYUiJZejinuQ%2FMhm6yj0jXd4Fe3%2F0atL4LTs19sLtVi1kamiPJ1aZOx8xps8ZAKa2tvQinTyXYUnONVmpAhqXkYPzN0ENn8gCoZcUXmCkSFbNGAnJDY1ZzC8oJRtvg%2F8AodmodwOHSO2RS042fIpp7FOYL7HFIjw0LHVts1voas9XUR8A0NDTh5xXEVSEZJUV%2BhVGgeRThbBgA3hfCIalr6jWx5RkUF5SbqQQGybMus9sHyUpRoUiwlj7YFFRgg5sPjshliGT7gPaxBxwNvOFk6AsXo2BY6drjL9NmkGDzNOdB4BMyIRgAjbST2fH8jeI%2FgqxOtboaB%2BHEChFiuamXVQILxDErjxdXDjOVcOOAvTWr6RlsquthJyoEYx5X5bB0NgrE9oXmJNgivUWQJ182V0nyRgRBzBZZx9a6bxC%2B9NoWOtDU08b1%2FVryq9BC%2BuiMm5xDjZ8ill2SOlN7bbTru8h%2BY7X3g1nVBtpKP9B1TqjAsQ0F2PzhN9j5j12vCIL4m3JeANPu1dWtiqInyiV28VcJgpgZw5Yl8D8Pg6dpGPhbxDixWghZ0KVTVCZSP4SE6%2FOKbg0x25%2BPhRrt2k449vwIdnARWAuUYKrXjPIA5X4uIPx3RzwW2UjLgq1i7nlsFf3GWSaA22ciSF1SpARBLJQcii%2B7mlDE99oW9ovnWglCD0vvAJhO5SIlTrt62lm5CSaqWm0nuZyc7l9gwuVhp8a%2F4WwoUL3%2FDPig6fNA1%2FUAa7EPmHpecNB9fZE76LqDvzYUycLL%2FLc5Y1iyEJlvulDlvqQq72TCGldFFcFoGxgfqrkXnCc%2FwdiWQfaS%2BrQPcFDfCRwDAWiUyk1kBx0ePDaFIjkwY%2FxXxUQKlF%2B5xHn845iqKQ9edscQVtnYqpxZM6eow0LPlnwAOuhN%2FiZTXtpKME9jCWI4yTJq7klVGBK9%2FHhO6Qtmx5AKGLf7JUw47mMO0%2FkL58OZ7ZJ10jq%2B0s04hHxK2y10I5ft7XtMuZQMPS8cXxEMv%2B3hPyX5p%2FsRBDHp%2F4dxJY1OpLtWNYChrFzdU3A6hmB5OR9T1dRUoJYvXzksS7zgoRwDYFZNbqNdyonscsn%2BhGF30xC4mwjB%2B7sQHlvFr4KtYh%2Be9bgQ9OhJ8zJqvndSZHxagFvlI9CUu7R7GEY79oyp1j4r6xcuHLcM6302pVYWe6t27tql%2BNbSvNGBRh%2FOupDHzU4u%2BbIwo2RLbUvtJxPuuD%2Bjs7FJFmHXCjq8rlzCzI8%2BGgM2kefPX4DU1LTd1Pneqyovns5XnOGMST6ZDZYaIjez8EvqXO9VhZmZB084MfqoySezoYERrlMv5P%2BHbKztiqjJp0%2BfjhkzZkBdXR1z5nyIuXPnQkvrr1i4YCE2m5rhdvnd9dR53rs83H1E%2B60ONOyzOnDj0AHbUntbeg7tKDPpmCNX5sI5IeG5evgLfIJ9hQFhzHBxNP1sbJL5n232%2FwJLsW2HAKR%2BMAAAAABJRU5ErkJggg%3D%3D&logoSize=auto
