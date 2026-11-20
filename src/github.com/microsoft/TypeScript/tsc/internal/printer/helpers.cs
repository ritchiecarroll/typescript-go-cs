namespace go.github.com.microsoft.TypeScript.tsc.@internal;

partial class printer_package {

partial struct Priority {
    public nint Value;
}

partial struct EmitHelper {
    public @string Name;
    public bool Scoped;
    public @string Text;
    public Func<Func<@string, @string>, @string> TextCallback;
    public ж<Priority> Priority;
    public slice<ж<EmitHelper>> Dependencies;
    public @string ImportName;
}

internal static nint compareEmitHelpers(ж<EmitHelper> Ꮡx, ж<EmitHelper> Ꮡy) {
    ref var x = ref Ꮡx.DerefOrNull();
    ref var y = ref Ꮡy.DerefOrNull();

    if (Ꮡx == Ꮡy) {
        return 0;
    }
    if (x.Priority == y.Priority) {
        return 0;
    }
    if (x.Priority == nil) {
        return 1;
    }
    if (y.Priority == nil) {
        return -1;
    }
    return (~x.Priority).Value - (~y.Priority).Value;
}

[GoLazy] partial class lazyᴛdecorateHelper { internal static ж<EmitHelper> decorateHelper = Ꮡ(new EmitHelper(
    Name: "typescript:decorate",
    ImportName: "__decorate",
    Scoped: false,
    Priority: Ꮡ(new Priority(2)),
    Text: """
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
"""u8.Raw()
)); }

[GoLazy] partial class lazyᴛmetadataHelper { internal static ж<EmitHelper> metadataHelper = Ꮡ(new EmitHelper(
    Name: "typescript:metadata",
    ImportName: "__metadata",
    Scoped: false,
    Priority: Ꮡ(new Priority(3)),
    Text: """
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
"""u8.Raw()
)); }

[GoLazy] partial class lazyᴛparamHelper { internal static ж<EmitHelper> paramHelper = Ꮡ(new EmitHelper(
    Name: "typescript:param",
    ImportName: "__param",
    Scoped: false,
    Priority: Ꮡ(new Priority(4)),
    Text: """
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
"""u8.Raw()
)); }

internal static ж<EmitHelper> addDisposableResourceHelper = Ꮡ(new EmitHelper(
    Name: "typescript:addDisposableResource",
    ImportName: "__addDisposableResource",
    Scoped: false,
    Text: """
var __addDisposableResource = (this && this.__addDisposableResource) || function (env, value, async) {
    if (value !== null && value !== void 0) {
        if (typeof value !== "object" && typeof value !== "function") throw new TypeError("Object expected.");
        var dispose, inner;
        if (async) {
            if (!Symbol.asyncDispose) throw new TypeError("Symbol.asyncDispose is not defined.");
            dispose = value[Symbol.asyncDispose];
        }
        if (dispose === void 0) {
            if (!Symbol.dispose) throw new TypeError("Symbol.dispose is not defined.");
            dispose = value[Symbol.dispose];
            if (async) inner = dispose;
        }
        if (typeof dispose !== "function") throw new TypeError("Object not disposable.");
        if (inner) dispose = function() { try { inner.call(this); } catch (e) { return Promise.reject(e); } };
        env.stack.push({ value: value, dispose: dispose, async: async });
    }
    else if (async) {
        env.stack.push({ async: true });
    }
    return value;
};
"""u8.Raw()
));

internal static ж<EmitHelper> disposeResourcesHelper = Ꮡ(new EmitHelper(
    Name: "typescript:disposeResources",
    ImportName: "__disposeResources",
    Scoped: false,
    Text: """
var __disposeResources = (this && this.__disposeResources) || (function (SuppressedError) {
    return function (env) {
        function fail(e) {
            env.error = env.hasError ? new SuppressedError(e, env.error, "An error was suppressed during disposal.") : e;
            env.hasError = true;
        }
        var r, s = 0;
        function next() {
            while (r = env.stack.pop()) {
                try {
                    if (!r.async && s === 1) return s = 0, env.stack.push(r), Promise.resolve().then(next);
                    if (r.dispose) {
                        var result = r.dispose.call(r.value);
                        if (r.async) return s |= 2, Promise.resolve(result).then(next, function(e) { fail(e); return next(); });
                    }
                    else s |= 1;
                }
                catch (e) {
                    fail(e);
                }
            }
            if (s === 1) return env.hasError ? Promise.reject(env.error) : Promise.resolve();
            if (env.hasError) throw env.error;
        }
        return next();
    };
})(typeof SuppressedError === "function" ? SuppressedError : function (error, suppressed, message) {
    var e = new Error(message);
    return e.name = "SuppressedError", e.error = error, e.suppressed = suppressed, e;
});
"""u8.Raw()
));

internal static ж<EmitHelper> classPrivateFieldGetHelper = Ꮡ(new EmitHelper(
    Name: "typescript:classPrivateFieldGet",
    ImportName: "__classPrivateFieldGet",
    Scoped: false,
    Text: """
var __classPrivateFieldGet = (this && this.__classPrivateFieldGet) || function (receiver, state, kind, f) {
    if (kind === "a" && !f) throw new TypeError("Private accessor was defined without a getter");
    if (typeof state === "function" ? receiver !== state || !f : !state.has(receiver)) throw new TypeError("Cannot read private member from an object whose class did not declare it");
    return kind === "m" ? f : kind === "a" ? f.call(receiver) : f ? f.value : state.get(receiver);
};
"""u8.Raw()
));

internal static ж<EmitHelper> classPrivateFieldSetHelper = Ꮡ(new EmitHelper(
    Name: "typescript:classPrivateFieldSet",
    ImportName: "__classPrivateFieldSet",
    Scoped: false,
    Text: """
var __classPrivateFieldSet = (this && this.__classPrivateFieldSet) || function (receiver, state, value, kind, f) {
    if (kind === "m") throw new TypeError("Private method is not writable");
    if (kind === "a" && !f) throw new TypeError("Private accessor was defined without a setter");
    if (typeof state === "function" ? receiver !== state || !f : !state.has(receiver)) throw new TypeError("Cannot write private member to an object whose class did not declare it");
    return (kind === "a" ? f.call(receiver, value) : f ? f.value = value : state.set(receiver, value)), value;
};
"""u8.Raw()
));

internal static ж<EmitHelper> classPrivateFieldInHelper = Ꮡ(new EmitHelper(
    Name: "typescript:classPrivateFieldIn",
    ImportName: "__classPrivateFieldIn",
    Scoped: false,
    Text: """
var __classPrivateFieldIn = (this && this.__classPrivateFieldIn) || function(state, receiver) {
    if (receiver === null || (typeof receiver !== "object" && typeof receiver !== "function")) throw new TypeError("Cannot use 'in' operator on non-object");
    return typeof state === "function" ? receiver === state : state.has(receiver);
};
"""u8.Raw()
));

internal static ж<EmitHelper> awaitHelper = Ꮡ(new EmitHelper(
    Name: "typescript:await",
    ImportName: "__await",
    Scoped: false,
    Text: @"var __await = (this && this.__await) || function (v) { return this instanceof __await ? (this.v = v, this) : new __await(v); }"
));

[GoLazy] partial class lazyᴛasyncGeneratorHelper { internal static ж<EmitHelper> asyncGeneratorHelper = Ꮡ(new EmitHelper(
    Name: "typescript:asyncGenerator",
    ImportName: "__asyncGenerator",
    Scoped: false,
    Dependencies: [awaitHelper],
    Text: """
var __asyncGenerator = (this && this.__asyncGenerator) || function (thisArg, _arguments, generator) {
    if (!Symbol.asyncIterator) throw new TypeError("Symbol.asyncIterator is not defined.");
    var g = generator.apply(thisArg, _arguments || []), i, q = [];
    return i = Object.create((typeof AsyncIterator === "function" ? AsyncIterator : Object).prototype), verb("next"), verb("throw"), verb("return", awaitReturn), i[Symbol.asyncIterator] = function () { return this; }, i;
    function awaitReturn(f) { return function (v) { return Promise.resolve(v).then(f, reject); }; }
    function verb(n, f) { if (g[n]) { i[n] = function (v) { return new Promise(function (a, b) { q.push([n, v, a, b]) > 1 || resume(n, v); }); }; if (f) i[n] = f(i[n]); } }
    function resume(n, v) { try { step(g[n](v)); } catch (e) { settle(q[0][3], e); } }
    function step(r) { r.value instanceof __await ? Promise.resolve(r.value.v).then(fulfill, reject) : settle(q[0][2], r); }
    function fulfill(value) { resume("next", value); }
    function reject(value) { resume("throw", value); }
    function settle(f, v) { if (f(v), q.shift(), q.length) resume(q[0][0], q[0][1]); }
};
"""u8.Raw()
)); }

[GoLazy] partial class lazyᴛasyncDelegatorHelper { internal static ж<EmitHelper> asyncDelegatorHelper = Ꮡ(new EmitHelper(
    Name: "typescript:asyncDelegator",
    ImportName: "__asyncDelegator",
    Scoped: false,
    Dependencies: [awaitHelper],
    Text: """
var __asyncDelegator = (this && this.__asyncDelegator) || function (o) {
    var i, p;
    return i = {}, verb("next"), verb("throw", function (e) { throw e; }), verb("return"), i[Symbol.iterator] = function () { return this; }, i;
    function verb(n, f) { i[n] = o[n] ? function (v) { return (p = !p) ? { value: __await(o[n](v)), done: false } : f ? f(v) : v; } : f; }
};
"""u8.Raw()
)); }

internal static ж<EmitHelper> asyncValuesHelper = Ꮡ(new EmitHelper(
    Name: "typescript:asyncValues",
    ImportName: "__asyncValues",
    Scoped: false,
    Text: """
var __asyncValues = (this && this.__asyncValues) || function (o) {
    if (!Symbol.asyncIterator) throw new TypeError("Symbol.asyncIterator is not defined.");
    var m = o[Symbol.asyncIterator], i;
    return m ? m.call(o) : (o = typeof __values === "function" ? __values(o) : o[Symbol.iterator](), i = {}, verb("next"), verb("throw"), verb("return"), i[Symbol.asyncIterator] = function () { return this; }, i);
    function verb(n) { i[n] = o[n] && function (v) { return new Promise(function (resolve, reject) { v = o[n](v), settle(resolve, reject, v.done, v.value); }); }; }
    function settle(resolve, reject, d, v) { Promise.resolve(v).then(function(v) { resolve({ value: v, done: d }); }, reject); }
};
"""u8.Raw()
));

internal static ж<EmitHelper> restHelper = Ꮡ(new EmitHelper(
    Name: "typescript:rest",
    ImportName: "__rest",
    Scoped: false,
    Text: """
var __rest = (this && this.__rest) || function (s, e) {
    var t = {};
    for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p) && e.indexOf(p) < 0)
        t[p] = s[p];
    if (s != null && typeof Object.getOwnPropertySymbols === "function")
        for (var i = 0, p = Object.getOwnPropertySymbols(s); i < p.length; i++) {
            if (e.indexOf(p[i]) < 0 && Object.prototype.propertyIsEnumerable.call(s, p[i]))
                t[p[i]] = s[p[i]];
        }
    return t;
};
"""u8.Raw()
));

[GoLazy] partial class lazyᴛawaiterHelper { internal static ж<EmitHelper> awaiterHelper = Ꮡ(new EmitHelper(
    Name: "typescript:awaiter",
    ImportName: "__awaiter",
    Scoped: false,
    Priority: Ꮡ(new Priority(5)),
    Text: """
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
"""u8.Raw()
)); }

[GoLazy] partial class lazyᴛAsyncSuperHelper { public static ж<EmitHelper> AsyncSuperHelper = Ꮡ(new EmitHelper(
    Name: "typescript:async-super",
    Scoped: true,
    TextCallback: (Func<@string, @string> makeUniqueName) => "\nconst "u8 + makeUniqueName("_superIndex") + " = name => super[name];"u8
)); }

[GoLazy] partial class lazyᴛAdvancedAsyncSuperHelper { public static ж<EmitHelper> AdvancedAsyncSuperHelper = Ꮡ(new EmitHelper(
    Name: "typescript:advanced-async-super",
    Scoped: true,
    TextCallback: (Func<@string, @string> makeUniqueName) => "\nconst "u8 + makeUniqueName("_superIndex") + " = (function (geti, seti) {\n"u8 + "    const cache = Object.create(null);\n"u8 + "    return name => cache[name] || (cache[name] = { get value() { return geti(name); }, set value(v) { seti(name, v); } });\n"u8 + "})(name => super[name], (name, value) => super[name] = value);"u8
)); }

[GoLazy] partial class lazyᴛesDecorateHelper { internal static ж<EmitHelper> esDecorateHelper = Ꮡ(new EmitHelper(
    Name: "typescript:esDecorate",
    ImportName: "__esDecorate",
    Scoped: false,
    Priority: Ꮡ(new Priority(2)),
    Text: """
var __esDecorate = (this && this.__esDecorate) || function (ctor, descriptorIn, decorators, contextIn, initializers, extraInitializers) {
    function accept(f) { if (f !== void 0 && typeof f !== "function") throw new TypeError("Function expected"); return f; }
    var kind = contextIn.kind, key = kind === "getter" ? "get" : kind === "setter" ? "set" : "value";
    var target = !descriptorIn && ctor ? contextIn["static"] ? ctor : ctor.prototype : null;
    var descriptor = descriptorIn || (target ? Object.getOwnPropertyDescriptor(target, contextIn.name) : {});
    var _, done = false;
    for (var i = decorators.length - 1; i >= 0; i--) {
        var context = {};
        for (var p in contextIn) context[p] = p === "access" ? {} : contextIn[p];
        for (var p in contextIn.access) context.access[p] = contextIn.access[p];
        context.addInitializer = function (f) { if (done) throw new TypeError("Cannot add initializers after decoration has completed"); extraInitializers.push(accept(f || null)); };
        var result = (0, decorators[i])(kind === "accessor" ? { get: descriptor.get, set: descriptor.set } : descriptor[key], context);
        if (kind === "accessor") {
            if (result === void 0) continue;
            if (result === null || typeof result !== "object") throw new TypeError("Object expected");
            if (_ = accept(result.get)) descriptor.get = _;
            if (_ = accept(result.set)) descriptor.set = _;
            if (_ = accept(result.init)) initializers.unshift(_);
        }
        else if (_ = accept(result)) {
            if (kind === "field") initializers.unshift(_);
            else descriptor[key] = _;
        }
    }
    if (target) Object.defineProperty(target, contextIn.name, descriptor);
    done = true;
};
"""u8.Raw()
)); }

[GoLazy] partial class lazyᴛrunInitializersHelper { internal static ж<EmitHelper> runInitializersHelper = Ꮡ(new EmitHelper(
    Name: "typescript:runInitializers",
    ImportName: "__runInitializers",
    Scoped: false,
    Priority: Ꮡ(new Priority(2)),
    Text: """
var __runInitializers = (this && this.__runInitializers) || function (thisArg, initializers, value) {
    var useValue = arguments.length > 2;
    for (var i = 0; i < initializers.length; i++) {
        value = useValue ? initializers[i].call(thisArg, value) : initializers[i].call(thisArg);
    }
    return useValue ? value : void 0;
};
"""u8.Raw()
)); }

[GoLazy] partial class lazyᴛmakeTemplateObjectHelper { internal static ж<EmitHelper> makeTemplateObjectHelper = Ꮡ(new EmitHelper(
    Name: "typescript:makeTemplateObject",
    ImportName: "__makeTemplateObject",
    Scoped: false,
    Priority: Ꮡ(new Priority(0)),
    Text: """
var __makeTemplateObject = (this && this.__makeTemplateObject) || function (cooked, raw) {
    if (Object.defineProperty) { Object.defineProperty(cooked, "raw", { value: raw }); } else { cooked.raw = raw; }
    return cooked;
};
"""u8.Raw()
)); }

internal static ж<EmitHelper> propKeyHelper = Ꮡ(new EmitHelper(
    Name: "typescript:propKey",
    ImportName: "__propKey",
    Scoped: false,
    Text: """
var __propKey = (this && this.__propKey) || function (x) {
    return typeof x === "symbol" ? x : "".concat(x);
};
"""u8.Raw()
));

internal static ж<EmitHelper> setFunctionNameHelper = Ꮡ(new EmitHelper(
    Name: "typescript:setFunctionName",
    ImportName: "__setFunctionName",
    Scoped: false,
    Text: """
var __setFunctionName = (this && this.__setFunctionName) || function (f, name, prefix) {
    if (typeof name === "symbol") name = name.description ? "[".concat(name.description, "]") : "";
    return Object.defineProperty(f, "name", { configurable: true, value: prefix ? "".concat(prefix, " ", name) : name });
};
"""u8.Raw()
));

[GoLazy] partial class lazyᴛcreateBindingHelper { internal static ж<EmitHelper> createBindingHelper = Ꮡ(new EmitHelper(
    Name: "typescript:commonjscreatebinding",
    ImportName: "__createBinding",
    Scoped: false,
    Priority: Ꮡ(new Priority(1)),
    Text: """
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
"""u8.Raw()
)); }

[GoLazy] partial class lazyᴛsetModuleDefaultHelper { internal static ж<EmitHelper> setModuleDefaultHelper = Ꮡ(new EmitHelper(
    Name: "typescript:commonjscreatevalue",
    ImportName: "__setModuleDefault",
    Scoped: false,
    Priority: Ꮡ(new Priority(1)),
    Text: """
var __setModuleDefault = (this && this.__setModuleDefault) || (Object.create ? (function(o, v) {
    Object.defineProperty(o, "default", { enumerable: true, value: v });
}) : function(o, v) {
    o["default"] = v;
});
"""u8.Raw()
)); }

[GoLazy] partial class lazyᴛimportStarHelper { internal static ж<EmitHelper> importStarHelper = Ꮡ(new EmitHelper(
    Name: "typescript:commonjsimportstar",
    ImportName: "__importStar",
    Scoped: false,
    Dependencies: [createBindingHelper, setModuleDefaultHelper],
    Priority: Ꮡ(new Priority(2)),
    Text: """
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
"""u8.Raw()
)); }

internal static ж<EmitHelper> importDefaultHelper = Ꮡ(new EmitHelper(
    Name: "typescript:commonjsimportdefault",
    ImportName: "__importDefault",
    Scoped: false,
    Text: """
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
"""u8.Raw()
));

[GoLazy] partial class lazyᴛexportStarHelper { internal static ж<EmitHelper> exportStarHelper = Ꮡ(new EmitHelper(
    Name: "typescript:export-star",
    ImportName: "__exportStar",
    Scoped: false,
    Dependencies: [createBindingHelper],
    Priority: Ꮡ(new Priority(2)),
    Text: """
var __exportStar = (this && this.__exportStar) || function(m, exports) {
    for (var p in m) if (p !== "default" && !Object.prototype.hasOwnProperty.call(exports, p)) __createBinding(exports, m, p);
};
"""u8.Raw()
)); }

internal static ж<EmitHelper> rewriteRelativeImportExtensionsHelper = Ꮡ(new EmitHelper(
    Name: "typescript:rewriteRelativeImportExtensions",
    ImportName: "__rewriteRelativeImportExtension",
    Scoped: false,
    Text: """
var __rewriteRelativeImportExtension = (this && this.__rewriteRelativeImportExtension) || function (path, preserveJsx) {
    if (typeof path === "string" && /^\.\.?\//.test(path)) {
        return path.replace(/\.(tsx)$|((?:\.d)?)((?:\.[^./]+?)?)\.([cm]?)ts$/i, function (m, tsx, d, ext, cm) {
            return tsx ? preserveJsx ? ".jsx" : ".js" : d && (!ext || !cm) ? m : (d + ext + "." + cm.toLowerCase() + "js");
        });
    }
    return path;
};
"""u8.Raw()
));

} // end printer_package
