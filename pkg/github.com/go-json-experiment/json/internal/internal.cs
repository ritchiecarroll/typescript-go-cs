// Copyright 2023 The Go Authors. All rights reserved.
// Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

namespace go.github.com.go_json_experiment.json;

using errors = errors_package;

partial class internal_package {

partial struct NotForPublicUse {
}

public static readonly ж<NotForPublicUse> ᏑAllowInternalUse = new StaticBox<NotForPublicUse>(default(NotForPublicUse));
public static ref NotForPublicUse AllowInternalUse => ref ((StaticBox<NotForPublicUse>)ᏑAllowInternalUse).ValueSlot;

public static error ErrCycle = errors.New("encountered a cycle");
public static error ErrNonNilReference = errors.New("value must be passed as a non-nil pointer reference");
public static error ErrNilInterface = errors.New("cannot derive concrete type for nil interface with finite type set");

public static readonly ж<Func<any, error, error>> ᏑTransformMarshalError = new StaticBox<Func<any, error, error>>(default(Func<any, error, error>));
public static ref Func<any, error, error> TransformMarshalError => ref ((StaticBox<Func<any, error, error>>)ᏑTransformMarshalError).ValueSlot;
public static readonly ж<Func<any, error, @string, error>> ᏑNewMarshalerError = new StaticBox<Func<any, error, @string, error>>(default(Func<any, error, @string, error>));
public static ref Func<any, error, @string, error> NewMarshalerError => ref ((StaticBox<Func<any, error, @string, error>>)ᏑNewMarshalerError).ValueSlot;
public static readonly ж<Func<any, error, error>> ᏑTransformUnmarshalError = new StaticBox<Func<any, error, error>>(default(Func<any, error, error>));
public static ref Func<any, error, error> TransformUnmarshalError => ref ((StaticBox<Func<any, error, error>>)ᏑTransformUnmarshalError).ValueSlot;
public static readonly ж<Func<any>> ᏑNewRawNumber = new StaticBox<Func<any>>(default(Func<any>));
public static ref Func<any> NewRawNumber => ref ((StaticBox<Func<any>>)ᏑNewRawNumber).ValueSlot;
public static readonly ж<Func<slice<byte>, any>> ᏑRawNumberOf = new StaticBox<Func<slice<byte>, any>>(default(Func<slice<byte>, any>));
public static ref Func<slice<byte>, any> RawNumberOf => ref ((StaticBox<Func<slice<byte>, any>>)ᏑRawNumberOf).ValueSlot;

} // end internal_package
