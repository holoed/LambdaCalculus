// * **********************************************************************************************
// * Copyright (c) Edmondo Pentangelo. 
// *
// * This source code is subject to terms and conditions of the Microsoft Public License. 
// * A copy of the license can be found in the License.html file at the root of this distribution. 
// * By using this source code in any fashion, you are agreeing to be bound by the terms of the 
// * Microsoft Public License.
// *
// * You must not remove this notice, or any other, from this software.
// * **********************************************************************************************

open Tokenizer
open Parser

let rec toString exp = match exp with
                       | Closure(x,y,_) -> toString(Lambda(x,y))
                       | Lambda(Letter(x),y) -> sprintf "(λ%c.%s)" x (toString y)
                       | Var(Letter(x)) -> sprintf "%c" x
                       | Apply(x, y) -> sprintf "(%s %s)" (toString x) (toString y)
