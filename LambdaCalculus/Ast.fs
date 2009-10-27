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

namespace LambdaCalculus
module Ast = 

    open Tokenizer

    // AST data type
    type exp = | Var of Token
               | Lambda of Token * exp
               | Apply of exp * exp

    // Generalised tail recursive fold over AST datatype (Catamorphism)
    let foldExpr varF lamF appF exp = 
        let rec Loop e cont = 
            match e with
            | Var (Letter x) -> cont (varF x)
            | Lambda (Letter x, body) -> Loop body (fun bodyAcc -> cont (lamF x bodyAcc))
            | Apply (l, r) -> Loop l (fun lAcc ->
                              Loop r (fun rAcc ->
                                      cont (appF lAcc rAcc)))
            | _ -> failwith "This should never happen."
        Loop exp (fun x -> x)