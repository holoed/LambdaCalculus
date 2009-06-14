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

open Parser
open Tokenizer
open AstToCode

let assoc x xs = 
    let (k,v) = List.find ( fun (k,v) -> k = x ) xs
    v 
    
let exists x xs = List.exists ( fun (k,v) -> k = x ) xs

let rec apply env e =
    match e with
    | Var s -> if (exists s env) then assoc s env else Var s
    | Lambda (s, e') -> Closure (s, e', env)
    | Closure _ -> e
    | Apply (e1, e2) -> 
        let v1 = apply env e1 in
        let v2 = apply env e2 in
        match v1 with
        | Closure (s, e3, env2) -> apply ((s, v2) :: env2) e3
        | _ -> failwith "Impossible"
        
let rec close env e = 
     match e with
     | Closure(arg, body, env) -> Lambda(arg, (close env body))
     | Var s -> if (exists s env) then assoc s env else Var s
     | Apply (e1, e2) -> Apply( (close env e1), (close env e2) )
     | Lambda(arg, body) -> Lambda(arg, close env body)
     
let rec reduce e =
    let reduce' e = 
        match e with
        | Apply(Closure(x, body, []), Var(x')) when x = x' -> body
        | Apply(Lambda(x, body), Var(x')) when x = x' -> body
        | Apply(a, b) -> Apply(reduce a, reduce b)
        | Lambda(x, body) -> Lambda(x, reduce body)
        | Closure(x, body, []) -> Lambda(x, reduce body)
        | Var x -> Var x
    let e' = reduce' e
    if (e' = e) then e else reduce e'
    
let interpret e = e
                  |> tokenize
                  |> parse
                  |> apply []
                  |> close []
                  |> reduce
                  |> toString

    