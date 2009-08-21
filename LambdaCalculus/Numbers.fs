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

open Parser

let rec ToNumber exp = match exp with
                       | Lambda(f, Lambda(x, Var x')) when x = x' -> 0
                       | Lambda(f, Lambda(x, Apply(Var f', exp''))) when f = f' -> 
                                1 + ToNumber (Lambda(f, Lambda(x, exp'')))
                       | _ -> failwith "NaN"

