﻿// * **********************************************************************************************
// * Copyright (c) Edmondo Pentangelo. 
// *
// * This source code is subject to terms and conditions of the Microsoft Public License. 
// * A copy of the license can be found in the License.html file at the root of this distribution. 
// * By using this source code in any fashion, you are agreeing to be bound by the terms of the 
// * Microsoft Public License.
// *
// * You must not remove this notice, or any other, from this software.
// * **********************************************************************************************

namespace LambdaCalculus
module AstToCode =

    open Tokenizer
    open Ast

    let toString =
        foldExpr
            (fun x -> sprintf "%c" x)
            (fun x y -> sprintf "(λ%c.%s)" x y)
            (fun x y -> sprintf "(%s %s)" x y)