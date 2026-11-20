# typescript-go-cs: proof

TypeScript's Go compiler packages, converted to C# by [go2cs](https://github.com/ritchiecarroll/go2cs), compared with the
native Go build of the same sources.

*Measured 2026-11-20, linux/amd64.*

| Side | Build |
|:--|:--|
| Native | go1.26.11 |
| Converted | go2cs `5dd5cafd5b`, go.* `1.26.11.1`, .NET SDK 10.0.400 |

TypeScript is [microsoft/TypeScript](https://github.com/microsoft/TypeScript/tree/ba29a562991ceb942481e2a00810b4e5e4062e52/tsc) at `ba29a562991ceb942481e2a00810b4e5e4062e52`.

Every figure on this page is counted from the two sides' outputs, which are in `proof/`. None is estimated.

**Hand edits to the converted C#:** None. Every C# file under `src/` and `pkg/` is the converter's output, unedited.

## 1. The parse demo

`parsedemo samples/good.ts samples/bad.ts` prints, for each file:

- each parse diagnostic: code, start, length and message;
- every syntax node's kind and `[pos,end]`, in pre-order.

`good.ts` is a small valid file. `bad.ts` has syntax errors.

**Result: byte-identical.**

| Side | SHA-256 of the output |
|:--|:--|
| Native Go | `3d40bddc3d75717f5da37d7fbcf0c25bfd6a731aaf0d51174109c070e0d002ae` |
| Converted C# | `3d40bddc3d75717f5da37d7fbcf0c25bfd6a731aaf0d51174109c070e0d002ae` |

<details><summary>Native Go output (211 lines)</summary>

```
file good.ts (620 bytes)
KindSourceFile [0,620]
  KindInterfaceDeclaration [0,75]
    KindIdentifier [44,50]
    KindPropertySignature [52,63]
      KindIdentifier [52,54]
      KindNumberKeyword [55,62]
    KindPropertySignature [63,73]
      KindIdentifier [63,65]
      KindNumberKeyword [66,73]
  KindFunctionDeclaration [75,205]
    KindIdentifier [85,90]
    KindParameter [91,99]
      KindIdentifier [91,92]
      KindTypeReference [93,99]
        KindIdentifier [93,99]
    KindParameter [100,109]
      KindIdentifier [100,102]
      KindTypeReference [103,109]
        KindIdentifier [103,109]
    KindNumberKeyword [111,118]
    KindBlock [118,205]
      KindVariableStatement [120,162]
        KindVariableDeclarationList [120,161]
          KindVariableDeclaration [130,145]
            KindIdentifier [130,133]
            KindBinaryExpression [135,145]
              KindPropertyAccessExpression [135,139]
                KindIdentifier [135,137]
                KindIdentifier [138,139]
              KindMinusToken [139,141]
              KindPropertyAccessExpression [141,145]
                KindIdentifier [141,143]
                KindIdentifier [144,145]
          KindVariableDeclaration [146,161]
            KindIdentifier [146,149]
            KindBinaryExpression [151,161]
              KindPropertyAccessExpression [151,155]
                KindIdentifier [151,153]
                KindIdentifier [154,155]
              KindMinusToken [155,157]
              KindPropertyAccessExpression [157,161]
                KindIdentifier [157,159]
                KindIdentifier [160,161]
      KindReturnStatement [162,203]
        KindCallExpression [173,202]
          KindPropertyAccessExpression [173,183]
            KindIdentifier [173,178]
            KindIdentifier [179,183]
          KindBinaryExpression [184,201]
            KindBinaryExpression [184,191]
              KindIdentifier [184,186]
              KindAsteriskToken [186,188]
              KindIdentifier [188,191]
            KindPlusToken [191,193]
            KindBinaryExpression [193,201]
              KindIdentifier [193,196]
              KindAsteriskToken [196,198]
              KindIdentifier [198,201]
  KindClassDeclaration [205,488]
    KindExportKeyword [205,213]
    KindIdentifier [219,224]
    KindTypeParameter [225,240]
      KindIdentifier [225,226]
      KindTypeReference [234,240]
        KindIdentifier [234,240]
    KindPropertyDeclaration [243,270]
      KindPrivateKeyword [243,255]
      KindIdentifier [255,259]
      KindArrayType [260,264]
        KindTypeReference [260,262]
          KindIdentifier [260,262]
      KindArrayLiteralExpression [266,269]
    KindMethodDeclaration [270,325]
      KindIdentifier [270,278]
      KindParameter [279,283]
        KindIdentifier [279,280]
        KindTypeReference [281,283]
          KindIdentifier [281,283]
      KindThisType [285,290]
      KindBlock [290,325]
        KindExpressionStatement [292,310]
          KindCallExpression [292,309]
            KindPropertyAccessExpression [292,306]
              KindPropertyAccessExpression [292,301]
                KindThisKeyword [292,297]
                KindIdentifier [298,301]
              KindIdentifier [302,306]
            KindIdentifier [307,308]
        KindReturnStatement [310,323]
          KindThisKeyword [317,322]
    KindGetAccessor [325,486]
      KindIdentifier [333,340]
      KindNumberKeyword [343,350]
      KindBlock [350,486]
        KindVariableStatement [352,371]
          KindVariableDeclarationList [352,370]
            KindVariableDeclaration [364,370]
              KindIdentifier [364,366]
              KindNumericLiteral [368,370]
        KindForStatement [371,462]
          KindVariableDeclarationList [385,394]
            KindVariableDeclaration [388,394]
              KindIdentifier [388,390]
              KindNumericLiteral [392,394]
          KindBinaryExpression [395,415]
            KindIdentifier [395,397]
            KindLessThanToken [397,399]
            KindPropertyAccessExpression [399,415]
              KindPropertyAccessExpression [399,408]
                KindThisKeyword [399,404]
                KindIdentifier [405,408]
              KindIdentifier [409,415]
          KindPostfixUnaryExpression [416,420]
            KindIdentifier [416,418]
          KindExpressionStatement [421,462]
            KindBinaryExpression [421,461]
              KindIdentifier [421,423]
              KindPlusEqualsToken [423,426]
              KindCallExpression [426,461]
                KindIdentifier [426,431]
                KindElementAccessExpression [432,447]
                  KindPropertyAccessExpression [432,440]
                    KindThisKeyword [432,436]
                    KindIdentifier [437,440]
                  KindBinaryExpression [441,446]
                    KindIdentifier [441,442]
                    KindMinusToken [442,444]
                    KindNumericLiteral [444,446]
                KindElementAccessExpression [448,460]
                  KindPropertyAccessExpression [448,457]
                    KindThisKeyword [448,453]
                    KindIdentifier [454,457]
                  KindIdentifier [458,459]
        KindReturnStatement [462,480]
          KindIdentifier [477,479]
  KindVariableStatement [488,551]
    KindVariableDeclarationList [488,550]
      KindVariableDeclaration [495,550]
        KindIdentifier [495,497]
        KindCallExpression [499,550]
          KindPropertyAccessExpression [499,534]
            KindCallExpression [499,530]
              KindPropertyAccessExpression [499,514]
                KindNewExpression [499,510]
                  KindIdentifier [503,508]
                KindIdentifier [511,514]
              KindObjectLiteralExpression [515,529]
                KindPropertyAssignment [516,521]
                  KindIdentifier [516,518]
                  KindNumericLiteral [519,521]
                KindPropertyAssignment [522,527]
                  KindIdentifier [522,524]
                  KindNumericLiteral [525,527]
            KindIdentifier [531,534]
          KindObjectLiteralExpression [535,549]
            KindPropertyAssignment [536,541]
              KindIdentifier [536,538]
              KindNumericLiteral [539,541]
            KindPropertyAssignment [542,547]
              KindIdentifier [542,544]
              KindNumericLiteral [545,547]
  KindExpressionStatement [551,619]
    KindCallExpression [551,618]
      KindPropertyAccessExpression [551,563]
        KindIdentifier [551,559]
        KindIdentifier [560,563]
      KindTemplateExpression [564,584]
        KindTemplateHead [564,574]
        KindTemplateSpan [574,584]
          KindPropertyAccessExpression [574,582]
            KindIdentifier [574,575]
            KindIdentifier [576,582]
          KindTemplateTail [582,584]
      KindConditionalExpression [585,617]
        KindBinaryExpression [585,603]
          KindIdentifier [585,587]
          KindInstanceOfKeyword [587,598]
          KindIdentifier [598,603]
        KindQuestionToken [603,605]
        KindStringLiteral [605,610]
        KindColonToken [610,612]
        KindStringLiteral [612,617]
  KindEndOfFile [619,620]
file bad.ts (38 bytes)
diag TS1109 [6,+1] Expression expected.
diag TS1005 [31,+1] ':' expected.
diag TS1005 [33,+1] ':' expected.
diag TS1109 [34,+1] Expression expected.
diag TS1128 [36,+1] Declaration or statement expected.
KindSourceFile [0,38]
  KindExpressionStatement [0,7]
    KindBinaryExpression [0,5]
      KindIdentifier [0,3]
      KindEqualsToken [3,5]
      KindIdentifier [5,5]
  KindFunctionDeclaration [7,32]
    KindIdentifier [16,18]
    KindParameter [19,32]
      KindObjectBindingPattern [19,32]
        KindBindingElement [21,30]
          KindIdentifier [21,30]
          KindIdentifier [30,30]
        KindBindingElement [30,32]
          KindNumericLiteral [30,32]
          KindIdentifier [32,32]
    KindBlock [32,32]
  KindExpressionStatement [32,35]
    KindPrefixUnaryExpression [32,34]
      KindIdentifier [34,34]
  KindEndOfFile [37,38]
```

</details>

<details><summary>Converted C# output</summary>

```
file good.ts (620 bytes)
KindSourceFile [0,620]
  KindInterfaceDeclaration [0,75]
    KindIdentifier [44,50]
    KindPropertySignature [52,63]
      KindIdentifier [52,54]
      KindNumberKeyword [55,62]
    KindPropertySignature [63,73]
      KindIdentifier [63,65]
      KindNumberKeyword [66,73]
  KindFunctionDeclaration [75,205]
    KindIdentifier [85,90]
    KindParameter [91,99]
      KindIdentifier [91,92]
      KindTypeReference [93,99]
        KindIdentifier [93,99]
    KindParameter [100,109]
      KindIdentifier [100,102]
      KindTypeReference [103,109]
        KindIdentifier [103,109]
    KindNumberKeyword [111,118]
    KindBlock [118,205]
      KindVariableStatement [120,162]
        KindVariableDeclarationList [120,161]
          KindVariableDeclaration [130,145]
            KindIdentifier [130,133]
            KindBinaryExpression [135,145]
              KindPropertyAccessExpression [135,139]
                KindIdentifier [135,137]
                KindIdentifier [138,139]
              KindMinusToken [139,141]
              KindPropertyAccessExpression [141,145]
                KindIdentifier [141,143]
                KindIdentifier [144,145]
          KindVariableDeclaration [146,161]
            KindIdentifier [146,149]
            KindBinaryExpression [151,161]
              KindPropertyAccessExpression [151,155]
                KindIdentifier [151,153]
                KindIdentifier [154,155]
              KindMinusToken [155,157]
              KindPropertyAccessExpression [157,161]
                KindIdentifier [157,159]
                KindIdentifier [160,161]
      KindReturnStatement [162,203]
        KindCallExpression [173,202]
          KindPropertyAccessExpression [173,183]
            KindIdentifier [173,178]
            KindIdentifier [179,183]
          KindBinaryExpression [184,201]
            KindBinaryExpression [184,191]
              KindIdentifier [184,186]
              KindAsteriskToken [186,188]
              KindIdentifier [188,191]
            KindPlusToken [191,193]
            KindBinaryExpression [193,201]
              KindIdentifier [193,196]
              KindAsteriskToken [196,198]
              KindIdentifier [198,201]
  KindClassDeclaration [205,488]
    KindExportKeyword [205,213]
    KindIdentifier [219,224]
    KindTypeParameter [225,240]
      KindIdentifier [225,226]
      KindTypeReference [234,240]
        KindIdentifier [234,240]
    KindPropertyDeclaration [243,270]
      KindPrivateKeyword [243,255]
      KindIdentifier [255,259]
      KindArrayType [260,264]
        KindTypeReference [260,262]
          KindIdentifier [260,262]
      KindArrayLiteralExpression [266,269]
    KindMethodDeclaration [270,325]
      KindIdentifier [270,278]
      KindParameter [279,283]
        KindIdentifier [279,280]
        KindTypeReference [281,283]
          KindIdentifier [281,283]
      KindThisType [285,290]
      KindBlock [290,325]
        KindExpressionStatement [292,310]
          KindCallExpression [292,309]
            KindPropertyAccessExpression [292,306]
              KindPropertyAccessExpression [292,301]
                KindThisKeyword [292,297]
                KindIdentifier [298,301]
              KindIdentifier [302,306]
            KindIdentifier [307,308]
        KindReturnStatement [310,323]
          KindThisKeyword [317,322]
    KindGetAccessor [325,486]
      KindIdentifier [333,340]
      KindNumberKeyword [343,350]
      KindBlock [350,486]
        KindVariableStatement [352,371]
          KindVariableDeclarationList [352,370]
            KindVariableDeclaration [364,370]
              KindIdentifier [364,366]
              KindNumericLiteral [368,370]
        KindForStatement [371,462]
          KindVariableDeclarationList [385,394]
            KindVariableDeclaration [388,394]
              KindIdentifier [388,390]
              KindNumericLiteral [392,394]
          KindBinaryExpression [395,415]
            KindIdentifier [395,397]
            KindLessThanToken [397,399]
            KindPropertyAccessExpression [399,415]
              KindPropertyAccessExpression [399,408]
                KindThisKeyword [399,404]
                KindIdentifier [405,408]
              KindIdentifier [409,415]
          KindPostfixUnaryExpression [416,420]
            KindIdentifier [416,418]
          KindExpressionStatement [421,462]
            KindBinaryExpression [421,461]
              KindIdentifier [421,423]
              KindPlusEqualsToken [423,426]
              KindCallExpression [426,461]
                KindIdentifier [426,431]
                KindElementAccessExpression [432,447]
                  KindPropertyAccessExpression [432,440]
                    KindThisKeyword [432,436]
                    KindIdentifier [437,440]
                  KindBinaryExpression [441,446]
                    KindIdentifier [441,442]
                    KindMinusToken [442,444]
                    KindNumericLiteral [444,446]
                KindElementAccessExpression [448,460]
                  KindPropertyAccessExpression [448,457]
                    KindThisKeyword [448,453]
                    KindIdentifier [454,457]
                  KindIdentifier [458,459]
        KindReturnStatement [462,480]
          KindIdentifier [477,479]
  KindVariableStatement [488,551]
    KindVariableDeclarationList [488,550]
      KindVariableDeclaration [495,550]
        KindIdentifier [495,497]
        KindCallExpression [499,550]
          KindPropertyAccessExpression [499,534]
            KindCallExpression [499,530]
              KindPropertyAccessExpression [499,514]
                KindNewExpression [499,510]
                  KindIdentifier [503,508]
                KindIdentifier [511,514]
              KindObjectLiteralExpression [515,529]
                KindPropertyAssignment [516,521]
                  KindIdentifier [516,518]
                  KindNumericLiteral [519,521]
                KindPropertyAssignment [522,527]
                  KindIdentifier [522,524]
                  KindNumericLiteral [525,527]
            KindIdentifier [531,534]
          KindObjectLiteralExpression [535,549]
            KindPropertyAssignment [536,541]
              KindIdentifier [536,538]
              KindNumericLiteral [539,541]
            KindPropertyAssignment [542,547]
              KindIdentifier [542,544]
              KindNumericLiteral [545,547]
  KindExpressionStatement [551,619]
    KindCallExpression [551,618]
      KindPropertyAccessExpression [551,563]
        KindIdentifier [551,559]
        KindIdentifier [560,563]
      KindTemplateExpression [564,584]
        KindTemplateHead [564,574]
        KindTemplateSpan [574,584]
          KindPropertyAccessExpression [574,582]
            KindIdentifier [574,575]
            KindIdentifier [576,582]
          KindTemplateTail [582,584]
      KindConditionalExpression [585,617]
        KindBinaryExpression [585,603]
          KindIdentifier [585,587]
          KindInstanceOfKeyword [587,598]
          KindIdentifier [598,603]
        KindQuestionToken [603,605]
        KindStringLiteral [605,610]
        KindColonToken [610,612]
        KindStringLiteral [612,617]
  KindEndOfFile [619,620]
file bad.ts (38 bytes)
diag TS1109 [6,+1] Expression expected.
diag TS1005 [31,+1] ':' expected.
diag TS1005 [33,+1] ':' expected.
diag TS1109 [34,+1] Expression expected.
diag TS1128 [36,+1] Declaration or statement expected.
KindSourceFile [0,38]
  KindExpressionStatement [0,7]
    KindBinaryExpression [0,5]
      KindIdentifier [0,3]
      KindEqualsToken [3,5]
      KindIdentifier [5,5]
  KindFunctionDeclaration [7,32]
    KindIdentifier [16,18]
    KindParameter [19,32]
      KindObjectBindingPattern [19,32]
        KindBindingElement [21,30]
          KindIdentifier [21,30]
          KindIdentifier [30,30]
        KindBindingElement [30,32]
          KindNumericLiteral [30,32]
          KindIdentifier [32,32]
    KindBlock [32,32]
  KindExpressionStatement [32,35]
    KindPrefixUnaryExpression [32,34]
      KindIdentifier [34,34]
  KindEndOfFile [37,38]
```

</details>

## 2. The parser corpus

The corpus is exactly the input set TypeScript's own parser fuzz test (`FuzzParser` in `internal/parser`) runs.
`go test` reports the same 17,422 subtests for it:

- 17,412 seed units, from two sources:
  - every file under `testdata/fixtures`;
  - every unit of every multi-file test case under `testdata/tests/cases/compiler` and `conformance`, split by
    TypeScript's own test-file parser;
- the 10 stored fuzz inputs in `internal/parser/testdata/fuzz/FuzzParser`, with their JSX and module-indicator flags.

`go/cmd/parsecorpus-extract` writes them out in that order and with those flags. It runs natively only, so both
sides read the same unit files.

Each unit is parsed as the fuzz test parses it, as `/index<ext>`. Both sides print one line per unit: the SHA-256 of
the unit's full dump (the same dump as the demo's), its diagnostic count and its node count. A unit matches when the
two lines are equal.

**Result: 17,422 of 17,422 units match.** On the native side the corpus holds 2,583,651 syntax nodes and 3,815
parse diagnostics. Native run: 5.0 s. Converted run: 22.1 s.

| Mismatch class | Units |
|:--|--:|
| (none) | 0 |

### Every mismatch

The first differing dump line is shown for each unit, native / converted. The full dumps are in `proof/mismatch/`.

| Unit | Class | Source | First difference |
|:--|:--|:--|:--|
| | (none) | | |

## 3. The type-check demo

`checkdemo samples/check.ts samples/good.ts` type-checks each file as `/main.ts` in its own program, with
TypeScript's bundled library files, and prints every diagnostic (category, code, file, start, length, message) and
the program's file count.

**Result: byte-identical.** Native SHA-256 `7881a8e0c0d9466cc230a039d93ea66807d9a371f9ce5466a55c94eab8aa4a90`, converted `7881a8e0c0d9466cc230a039d93ea66807d9a371f9ce5466a55c94eab8aa4a90`.

<details><summary>Native Go output</summary>

```
file check.ts (181 bytes)
semantic TS2322 /main.ts [4,+1] Type 'string' is not assignable to type 'number'.
semantic TS2322 /main.ts [81,+1] Type 'number' is not assignable to type 'string'.
semantic TS2322 /main.ts [171,+1] Type 'string' is not assignable to type 'number'.
files 83
file good.ts (620 bytes)
files 83
```

</details>

## 4. TypeScript's compiler tests: type-check and emit

The pool is every single-file `.ts` unit from `tests/cases/compiler` in the parser corpus's manifest:
7,404 files. Each is run alone as `/main.ts` in its own program, in batches of 20 per process, on both sides:

- through `checkdemo` (every diagnostic, as in section 3);
- through `emitdemo` (declaration emit on, LF newlines: every output file's text, plus the emit diagnostics).

Each side's output is split per file and compared file by file. A converted process that dies has its missing files
re-run one at a time, so a crash is charged to the file that causes it; the native side gets the same treatment.

**Type-check: 7,404 of 7,404 files match.** Native 306.2 s, converted 8,205.8 s.

| Mismatch class | Files |
|:--|--:|
| (none) | 0 |

No file differs.

**Emit: 7,404 of 7,404 files match.** Native 297.9 s, converted 8,267.6 s.

| Mismatch class | Files |
|:--|--:|
| (none) | 0 |

No file differs.

Times are for context only. They cover the last resumed run of each pass, not the whole pass, when a pass was
resumed.

## 5. The emit demo

`emitdemo samples/good.ts samples/bad.ts samples/emit.ts` prints every output file of each program.
`emit.ts` exercises enums, const enums, namespaces, parameter properties, private names, overloads, async functions,
generators, mapped types and labelled tuples.

**Result: byte-identical.** Native SHA-256 `f389781332e59c387a301d155ed04cbff086dd36d810b574cd2c332ca91e839e`, converted `f389781332e59c387a301d155ed04cbff086dd36d810b574cd2c332ca91e839e`.

## 6. Multi-file projects

`mfdemo` is a small `tsc`-like driver over TypeScript's `compiler` package. For each project directory it parses
`tsconfig.json`, builds the program, prints every diagnostic `tsc` would report (configuration, syntactic, global,
semantic, declaration), then emits the program and prints every output file.

- `samples/projects/shop`: `esnext` modules with `bundler` resolution, a `paths` alias, a JSON import, `typeRoots`,
  declaration maps and source maps, two deliberate type errors.
- `samples/projects/legacy`: CommonJS, `es2015` downlevel, a checked JavaScript file with JSDoc types, decorators, an
  unknown compiler option, a missing module and an unused local.

**Result: byte-identical.** Native SHA-256 `137a39c5686c3a3682732ea18075cc52c3c3d8854bd9215b45e3844cdc548c81`, converted `137a39c5686c3a3682732ea18075cc52c3c3d8854bd9215b45e3844cdc548c81`.

<details><summary>Native Go output</summary>

```
project shop (7 files)
files 69 (63 bundled)
source /proj/src/catalog.json
source /proj/src/models/product.ts
source /proj/src/models/cart.ts
source /proj/src/util/format.ts
source /proj/src/index.ts
source /proj/types/env/index.d.ts
diagnostics 2
diag /proj/src/index.ts TS2345 [440,+3] Argument of type 'string' is not assignable to parameter of type 'number'.
diag /proj/src/index.ts TS2741 [452,+3] Property 'price' is missing in type '{ sku: string; name: string; }' but required in type 'Product'.
skipped false, emit diagnostics 0, outputs 17
emit /proj/out/catalog.json (191 bytes)
[
    { "sku": "A-1", "name": "Pen", "price": { "amount": 1.5, "currency": "USD" } },
    { "sku": "B-2", "name": "Pad", "price": { "amount": 3.25, "currency": "USD" }, "tags": ["paper"] }
]

--- end /proj/out/catalog.json
emit /proj/out/index.d.ts (196 bytes)
import { Cart } from "@models/cart";
import type { Product } from "@models/product";
declare const cart: Cart;
declare const bad: Product;
export { cart, bad };
//# sourceMappingURL=index.d.ts.map
--- end /proj/out/index.d.ts
emit /proj/out/index.d.ts.map (301 bytes)
{"version":3,"file":"index.d.ts","sourceRoot":"","sources":["../src/index.ts"],"names":[],"mappings":"AACA,OAAO,EAAE,IAAI,EAAE,MAAM,cAAc,CAAC;AACpC,OAAO,KAAK,EAAE,OAAO,EAAE,MAAM,iBAAiB,CAAC;AAI/C,QAAA,MAAM,IAAI,MAAkB,CAAC;AAM7B,QAAA,MAAM,GAAG,EAAE,OAAqC,CAAC;AACjD,OAAO,EAAE,IAAI,EAAE,GAAG,EAAE,CAAC"}
--- end /proj/out/index.d.ts.map
emit /proj/out/index.js (489 bytes)
import catalog from "./catalog.json";
import { Cart } from "@models/cart";
import { money, log } from "./util/format";
const products = catalog;
const cart = new Cart("USD");
for (const p of products)
    cart.add(p, 2);
log("info" /* Level.Info */, BUILD_ID, money(cart.total(), cart.currency));
// Deliberate errors: a wrong argument type and a missing property.
cart.add(products[0], "3");
const bad = { sku: "C-3", name: "Ink" };
export { cart, bad };
//# sourceMappingURL=index.js.map
--- end /proj/out/index.js
emit /proj/out/index.js.map (672 bytes)
{"version":3,"file":"index.js","sourceRoot":"","sources":["../src/index.ts"],"names":[],"mappings":"AAAA,OAAO,OAAO,MAAM,gBAAgB,CAAC;AACrC,OAAO,EAAE,IAAI,EAAE,MAAM,cAAc,CAAC;AAEpC,OAAO,EAAE,KAAK,EAAE,GAAG,EAAS,MAAM,eAAe,CAAC;AAElD,MAAM,QAAQ,GAAG,OAAoB,CAAC;AACtC,MAAM,IAAI,GAAG,IAAI,IAAI,CAAC,KAAK,CAAC,CAAC;AAC7B,KAAK,MAAM,CAAC,IAAI,QAAQ;IAAE,IAAI,CAAC,GAAG,CAAC,CAAC,EAAE,CAAC,CAAC,CAAC;AACzC,GAAG,0BAAa,QAAQ,EAAE,KAAK,CAAC,IAAI,CAAC,KAAK,EAAE,EAAE,IAAI,CAAC,QAAQ,CAAC,CAAC,CAAC;AAE9D,mEAAmE;AACnE,IAAI,CAAC,GAAG,CAAC,QAAQ,CAAC,CAAC,CAAC,EAAE,GAAG,CAAC,CAAC;AAC3B,MAAM,GAAG,GAAY,EAAE,GAAG,EAAE,KAAK,EAAE,IAAI,EAAE,KAAK,EAAE,CAAC;AACjD,OAAO,EAAE,IAAI,EAAE,GAAG,EAAE,CAAC"}
--- end /proj/out/index.js.map
emit /proj/out/models/cart.d.ts (366 bytes)
import type { Product, Currency } from "./product";
import { Stock } from "./product";
export declare class Cart {
    readonly currency: Currency;
    private lines;
    constructor(currency: Currency);
    add(product: Product, qty?: number): this;
    total(): number;
    availability(stock: Record<string, number>): Stock[];
}
//# sourceMappingURL=cart.d.ts.map
--- end /proj/out/models/cart.d.ts
emit /proj/out/models/cart.d.ts.map (471 bytes)
{"version":3,"file":"cart.d.ts","sourceRoot":"","sources":["../../src/models/cart.ts"],"names":[],"mappings":"AAAA,OAAO,KAAK,EAAE,OAAO,EAAE,QAAQ,EAAE,MAAM,WAAW,CAAC;AACnD,OAAO,EAAE,KAAK,EAAW,MAAM,WAAW,CAAC;AAC3C,qBAAa,IAAI;aAEe,QAAQ,EAAE,QAAQ;IAD9C,OAAO,CAAC,KAAK,CAAwD;IACrE,YAA4B,QAAQ,EAAE,QAAQ,EAAI;IAClD,GAAG,CAAC,OAAO,EAAE,OAAO,EAAE,GAAG,SAAI,GAAG,IAAI,CAInC;IACD,KAAK,IAAI,MAAM,CAOd;IACD,YAAY,CAAC,KAAK,EAAE,MAAM,CAAC,MAAM,EAAE,MAAM,CAAC,GAAG,KAAK,EAAE,CAEnD;CACJ"}
--- end /proj/out/models/cart.d.ts.map
emit /proj/out/models/cart.js (857 bytes)
import { stockOf } from "./product";
export class Cart {
    currency;
    lines = new Map();
    constructor(currency) {
        this.currency = currency;
    }
    add(product, qty = 1) {
        const line = this.lines.get(product.sku);
        if (line)
            line.qty += qty;
        else
            this.lines.set(product.sku, { product, qty });
        return this;
    }
    total() {
        let sum = 0;
        for (const { product, qty } of this.lines.values()) {
            if (product.price.currency !== this.currency)
                throw new Error(`mixed currency: ${product.sku}`);
            sum += product.price.amount * qty;
        }
        return Math.round(sum * 100) / 100;
    }
    availability(stock) {
        return [...this.lines.keys()].map(sku => stockOf(stock[sku] ?? 0));
    }
}
//# sourceMappingURL=cart.js.map
--- end /proj/out/models/cart.js
emit /proj/out/models/cart.js.map (1119 bytes)
{"version":3,"file":"cart.js","sourceRoot":"","sources":["../../src/models/cart.ts"],"names":[],"mappings":"AACA,OAAO,EAAS,OAAO,EAAE,MAAM,WAAW,CAAC;AAC3C,MAAM,OAAO,IAAI;IAEe,QAAQ;IAD5B,KAAK,GAAG,IAAI,GAAG,EAA6C,CAAC;IACrE,YAA4B,QAAkB;wBAAlB,QAAQ;IAAa,CAAC;IAClD,GAAG,CAAC,OAAgB,EAAE,GAAG,GAAG,CAAC;QACzB,MAAM,IAAI,GAAG,IAAI,CAAC,KAAK,CAAC,GAAG,CAAC,OAAO,CAAC,GAAG,CAAC,CAAC;QACzC,IAAI,IAAI;YAAE,IAAI,CAAC,GAAG,IAAI,GAAG,CAAC;;YAAM,IAAI,CAAC,KAAK,CAAC,GAAG,CAAC,OAAO,CAAC,GAAG,EAAE,EAAE,OAAO,EAAE,GAAG,EAAE,CAAC,CAAC;QAC9E,OAAO,IAAI,CAAC;IAChB,CAAC;IACD,KAAK;QACD,IAAI,GAAG,GAAG,CAAC,CAAC;QACZ,KAAK,MAAM,EAAE,OAAO,EAAE,GAAG,EAAE,IAAI,IAAI,CAAC,KAAK,CAAC,MAAM,EAAE,EAAE,CAAC;YACjD,IAAI,OAAO,CAAC,KAAK,CAAC,QAAQ,KAAK,IAAI,CAAC,QAAQ;gBAAE,MAAM,IAAI,KAAK,CAAC,mBAAmB,OAAO,CAAC,GAAG,EAAE,CAAC,CAAC;YAChG,GAAG,IAAI,OAAO,CAAC,KAAK,CAAC,MAAM,GAAG,GAAG,CAAC;QACtC,CAAC;QACD,OAAO,IAAI,CAAC,KAAK,CAAC,GAAG,GAAG,GAAG,CAAC,GAAG,GAAG,CAAC;IACvC,CAAC;IACD,YAAY,CAAC,KAA6B;QACtC,OAAO,CAAC,GAAG,IAAI,CAAC,KAAK,CAAC,IAAI,EAAE,CAAC,CAAC,GAAG,CAAC,GAAG,CAAC,EAAE,CAAC,OAAO,CAAC,KAAK,CAAC,GAAG,CAAC,IAAI,CAAC,CAAC,CAAC,CAAC;IACvE,CAAC;CACJ"}
--- end /proj/out/models/cart.js.map
emit /proj/out/models/product.d.ts (365 bytes)
export type Currency = "USD" | "EUR";
export interface Product {
    readonly sku: string;
    name: string;
    price: {
        amount: number;
        currency: Currency;
    };
    tags?: string[];
}
export declare enum Stock {
    Out = 0,
    Low = 1,
    Plenty = 2
}
export declare function stockOf(qty: number): Stock;
//# sourceMappingURL=product.d.ts.map
--- end /proj/out/models/product.d.ts
emit /proj/out/models/product.d.ts.map (482 bytes)
{"version":3,"file":"product.d.ts","sourceRoot":"","sources":["../../src/models/product.ts"],"names":[],"mappings":"AAAA,MAAM,MAAM,QAAQ,GAAG,KAAK,GAAG,KAAK,CAAC;AACrC,MAAM,WAAW,OAAO;IACpB,QAAQ,CAAC,GAAG,EAAE,MAAM,CAAC;IACrB,IAAI,EAAE,MAAM,CAAC;IACb,KAAK,EAAE;QAAE,MAAM,EAAE,MAAM,CAAC;QAAC,QAAQ,EAAE,QAAQ,CAAA;KAAE,CAAC;IAC9C,IAAI,CAAC,EAAE,MAAM,EAAE,CAAC;CACnB;AACD,oBAAY,KAAK;IAAG,GAAG,IAAA;IAAE,GAAG,IAAA;IAAE,MAAM,IAAA;CAAE;AACtC,wBAAgB,OAAO,CAAC,GAAG,EAAE,MAAM,GAAG,KAAK,CAE1C"}
--- end /proj/out/models/product.d.ts.map
emit /proj/out/models/product.js (320 bytes)
export var Stock;
(function (Stock) {
    Stock[Stock["Out"] = 0] = "Out";
    Stock[Stock["Low"] = 1] = "Low";
    Stock[Stock["Plenty"] = 2] = "Plenty";
})(Stock || (Stock = {}));
export function stockOf(qty) {
    return qty <= 0 ? Stock.Out : qty < 5 ? Stock.Low : Stock.Plenty;
}
//# sourceMappingURL=product.js.map
--- end /proj/out/models/product.js
emit /proj/out/models/product.js.map (435 bytes)
{"version":3,"file":"product.js","sourceRoot":"","sources":["../../src/models/product.ts"],"names":[],"mappings":"AAOA,MAAM,CAAN,IAAY,KAA0B;AAAtC,WAAY,KAAK;IAAG,+BAAG,CAAA;IAAE,+BAAG,CAAA;IAAE,qCAAM,CAAA;AAAC,CAAC,EAA1B,KAAK,KAAL,KAAK,QAAqB;AACtC,MAAM,UAAU,OAAO,CAAC,GAAW;IAC/B,OAAO,GAAG,IAAI,CAAC,CAAC,CAAC,CAAC,KAAK,CAAC,GAAG,CAAC,CAAC,CAAC,GAAG,GAAG,CAAC,CAAC,CAAC,CAAC,KAAK,CAAC,GAAG,CAAC,CAAC,CAAC,KAAK,CAAC,MAAM,CAAC;AACrE,CAAC"}
--- end /proj/out/models/product.js.map
emit /proj/out/util/format.d.ts (252 bytes)
export declare function money(amount: number, currency: string): string;
export declare const enum Level {
    Info = "info",
    Warn = "warn"
}
export declare function log(level: Level, ...parts: unknown[]): void;
//# sourceMappingURL=format.d.ts.map
--- end /proj/out/util/format.d.ts
emit /proj/out/util/format.d.ts.map (322 bytes)
{"version":3,"file":"format.d.ts","sourceRoot":"","sources":["../../src/util/format.ts"],"names":[],"mappings":"AAAA,wBAAgB,KAAK,CAAC,MAAM,EAAE,MAAM,EAAE,QAAQ,EAAE,MAAM,GAAG,MAAM,CAE9D;AACD,0BAAkB,KAAK;IAAG,IAAI,SAAS;IAAE,IAAI,SAAS;CAAE;AACxD,wBAAgB,GAAG,CAAC,KAAK,EAAE,KAAK,EAAE,GAAG,KAAK,EAAE,OAAO,EAAE,GAAG,IAAI,CAE3D"}
--- end /proj/out/util/format.d.ts.map
emit /proj/out/util/format.js (207 bytes)
export function money(amount, currency) {
    return `${currency} ${amount.toFixed(2)}`;
}
export function log(level, ...parts) {
    console.log(`[${level}]`, ...parts);
}
//# sourceMappingURL=format.js.map
--- end /proj/out/util/format.js
emit /proj/out/util/format.js.map (352 bytes)
{"version":3,"file":"format.js","sourceRoot":"","sources":["../../src/util/format.ts"],"names":[],"mappings":"AAAA,MAAM,UAAU,KAAK,CAAC,MAAc,EAAE,QAAgB;IAClD,OAAO,GAAG,QAAQ,IAAI,MAAM,CAAC,OAAO,CAAC,CAAC,CAAC,EAAE,CAAC;AAC9C,CAAC;AAED,MAAM,UAAU,GAAG,CAAC,KAAY,EAAE,GAAG,KAAgB;IACjD,OAAO,CAAC,GAAG,CAAC,IAAI,KAAK,GAAG,EAAE,GAAG,KAAK,CAAC,CAAC;AACxC,CAAC"}
--- end /proj/out/util/format.js.map
project legacy (4 files)
files 22 (19 bundled)
source /proj/lib/queue.ts
source /proj/lib/math.js
source /proj/main.ts
diagnostics 5
diag /proj/lib/math.js TS2345 [269,+3] Argument of type 'string' is not assignable to parameter of type 'number'.
diag /proj/main.ts TS2307 [93,+15] Cannot find module './lib/nowhere' or its corresponding type declarations.
diag /proj/main.ts TS6133 [117,+6] 'unused' is declared but its value is never read.
diag /proj/main.ts TS18048 [291,+5] 'maybe' is possibly 'undefined'.
diag /proj/tsconfig.json TS5023 [291,+16] Unknown compiler option 'notARealOption'.
skipped false, emit diagnostics 0, outputs 6
emit /proj/dist/lib/math.d.ts (227 bytes)
declare const _exports: {
    gcd: typeof gcd;
    sum: (xs: number[]) => number;
};
export = _exports;
/**
 * @param {number} a
 * @param {number} b
 * @returns {number}
 */
declare function gcd(a: number, b: number): number;

--- end /proj/dist/lib/math.d.ts
emit /proj/dist/lib/math.js (290 bytes)
"use strict";
// @ts-check
/**
 * @param {number} a
 * @param {number} b
 * @returns {number}
 */
function gcd(a, b) { return b === 0 ? a : gcd(b, a % b); }
/** @type {(xs: number[]) => number} */
const sum = xs => xs.reduce((s, x) => s + x, 0);
module.exports = { gcd, sum };
gcd("1", 2);

--- end /proj/dist/lib/math.js
emit /proj/dist/lib/queue.d.ts (213 bytes)
export declare class Queue<T> {
    private items;
    push(...xs: T[]): number;
    shift(): T | undefined;
    drain(f: (x: T) => Promise<void>): Promise<number>;
    get size(): number;
}
export default Queue;

--- end /proj/dist/lib/queue.d.ts
emit /proj/dist/lib/queue.js (2033 bytes)
"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.Queue = void 0;
function sealed(ctor) { Object.seal(ctor); Object.seal(ctor.prototype); }
let Queue = class Queue {
    constructor() {
        this.items = [];
    }
    push(...xs) { return this.items.push(...xs); }
    shift() { return this.items.shift(); }
    drain(f) {
        return __awaiter(this, void 0, void 0, function* () {
            let n = 0;
            for (let x = this.shift(); x !== undefined; x = this.shift()) {
                yield f(x);
                n++;
            }
            return n;
        });
    }
    get size() { return this.items.length; }
};
exports.Queue = Queue;
exports.Queue = Queue = __decorate([
    sealed
], Queue);
exports.default = Queue;

--- end /proj/dist/lib/queue.js
emit /proj/dist/main.d.ts (11 bytes)
export {};

--- end /proj/dist/main.d.ts
emit /proj/dist/main.js (2725 bytes)
"use strict";
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    var desc = Object.getOwnPropertyDescriptor(m, k);
    if (!desc || ("get" in desc ? !m.__esModule : desc.writable || desc.configurable)) {
      desc = { enumerable: true, get: function() { return m[k]; } };
    }
    Object.defineProperty(o, k2, desc);
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __setModuleDefault = (this && this.__setModuleDefault) || (Object.create ? (function(o, v) {
    Object.defineProperty(o, "default", { enumerable: true, value: v });
}) : function(o, v) {
    o["default"] = v;
});
var __importStar = (this && this.__importStar) || (function () {
    var ownKeys = function(o) {
        ownKeys = Object.getOwnPropertyNames || function (o) {
            var ar = [];
            for (var k in o) if (Object.prototype.hasOwnProperty.call(o, k)) ar[ar.length] = k;
            return ar;
        };
        return ownKeys(o);
    };
    return function (mod) {
        if (mod && mod.__esModule) return mod;
        var result = {};
        if (mod != null) for (var k = ownKeys(mod), i = 0; i < k.length; i++) if (k[i] !== "default") __createBinding(result, mod, k[i]);
        __setModuleDefault(result, mod);
        return result;
    };
})();
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const queue_1 = __importDefault(require("./lib/queue"));
const math = __importStar(require("./lib/math"));
const nowhere_1 = require("./lib/nowhere");
const unused = 1;
const q = new queue_1.default();
q.push(12, 18, 27);
const g = math.gcd(q.shift(), q.shift());
let maybe;
console.log(g, math.sum([1, 2, 3]), maybe.length, nowhere_1.missing);
q.drain((x) => __awaiter(void 0, void 0, void 0, function* () { console.log(x); })).then(n => console.log(`drained ${n}`));

--- end /proj/dist/main.js
```

</details>

## 7. How the harness was checked

| Control | "Converted" side | Result |
|:--|:--|:--|
| Parse harness, positive | the native parse demo itself | demo identical; 17,422 of 17,422 units match |
| Parse harness, negative | the native parse demo with one character of unit `00010`'s corpus line changed | 17,421 of 17,422 match; the one mismatch named is `00010` |
| Compiler-test harness, positive | the native type-check demo itself (every 37th file, 201) | 201 of 201 match |
| Compiler-test harness, negative | the native type-check demo with one byte of file `00216`'s output changed | 200 of 201 match; the one mismatch named is `00216` |

## Reproducing

1. Check out TypeScript at `ba29a562991ceb942481e2a00810b4e5e4062e52`, and copy `go/cmd/*` into its `tsc/cmd/`.
2. In `tsc/`, build the native side from the unmodified module:

   ```
   for d in parsedemo checkdemo emitdemo mfdemo; do go build -o $d-go ./cmd/$d; done
   go run ./cmd/parsecorpus-extract . corpus
   ```

3. For the conversion only, add the one `go.mod` change MODIFICATIONS.md states (the go2cs fork of
   `github.com/zeebo/xxh3`), then convert with `go2cs -recurse=nuget` and build the demos as the README shows.
4. Compare:

   ```
   python proof/compare.py --native parsedemo-go --converted <parsedemo> --samples samples --corpus corpus --out results/parse
   python proof/demo_compare.py --name checkdemo --native checkdemo-go --converted <checkdemo> --cwd samples --out results -- check.ts good.ts
   python proof/demo_compare.py --name emitdemo --native emitdemo-go --converted <emitdemo> --cwd samples --out results -- good.ts bad.ts emit.ts
   python proof/demo_compare.py --name mfdemo --native mfdemo-go --converted <mfdemo> --cwd samples/projects --out results -- shop legacy
   python proof/typecheck_slice.py --native checkdemo-go --converted <checkdemo> --corpus corpus --out results/check-full --every 1 --timeout 3600
   python proof/typecheck_slice.py --native emitdemo-go --converted <emitdemo> --corpus corpus --out results/emit-full --every 1 --timeout 3600
   ```
