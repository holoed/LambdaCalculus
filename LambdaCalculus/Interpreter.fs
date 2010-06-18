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
module Interpreter =

    open Parser
    open Tokenizer
    open Ast
    open AstToCode
    open ContinuationMonad

    //Tail Recursive Version using continuation monad
    let subst x v e =
        let subst' (x,v,e) =
            let rec Loop (x, e) =
                cont { match e with
                       | Var(y) -> return (if x = y then v else e)
                       | Lambda(y, body) -> let x' = if x = y then Empty else x
                                            let! bodyAcc = Loop (x', body)
                                            return Lambda(y, bodyAcc)
                                            
                       | Apply(l, r) -> let! lacc = Loop (x, l)
                                        let! racc = Loop (x, r)
                                        return Apply (lacc, racc) }
            Loop (x, e) (fun x -> x)
        subst' (x,v,e)

    let rec reduce e =
        let rec Loop e =
            cont { match e with
                   | Var _ -> return e
                   | Lambda (s, e') -> let! eAcc' = Loop e'
                                       return Lambda (s, eAcc')
                                      
                   | Apply(e1, e2) -> match e1 with
                                      | Lambda(s, e3) -> return (subst s e2 e3)
                                      | _ -> let! e1Acc = Loop e1
                                             let! e2Acc = Loop e2
                                             return Apply (e1Acc, e2Acc) }
        Loop e (fun x -> x)

    let rec loop f x =
        let x' = f x
        if x = x' then x' else loop f x'

    let interpret e = e
                      |> tokenize
                      |> parse
                      |> loop reduce