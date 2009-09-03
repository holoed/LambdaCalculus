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

let toString exp =
    let rec Loop e cont =
           match e with
           | Lambda(Letter(x),y) -> Loop y (fun yacc -> cont (sprintf "(λ%c.%s)" x yacc))
           | Var(Letter(x)) -> cont (sprintf "%c" x)
           | Apply(x, y) -> Loop x (fun xacc ->
                            Loop y (fun yacc ->
                                    cont(sprintf "(%s %s)" xacc yacc)))
           | _ -> failwith "Invalid lambda expression"
    Loop exp (fun x -> x)

