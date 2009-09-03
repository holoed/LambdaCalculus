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

let ToNumber exp =
   let rec ToNumberAux exp acc =
                match exp with
                | Lambda(f, Lambda(x, Var x')) when x = x' -> acc
                | Lambda(f, Lambda(x, Apply(Var f', exp''))) when f = f' ->
                                ToNumberAux (Lambda(f, Lambda(x, exp''))) (acc + 1)
                | _ -> failwith "NaN"
   ToNumberAux exp 0


let FromNumber n =
    let folded = List.fold (fun exp _ -> Apply(Var (Letter 'f'), exp)) (Var (Letter 'x')) [1..n]
    Lambda(Letter 'f', Lambda(Letter 'x', folded))
 