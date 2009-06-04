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

let assoc x xs = 
    let (k,v) = List.find ( fun (k,v) -> k = x ) xs
    v 
    
let exists x xs = List.exists ( fun (k,v) -> k = x ) xs

let rec interpret env e =
    match e with
    | Var s -> if (exists s env) then assoc s env else Var s
    | Lambda (s, e') -> Closure (s, e', env)
    | Closure _ -> e
    | Apply (e1, e2) -> 
        let v1 = interpret env e1 in
        let v2 = interpret env e2 in
        match v1 with
        | Closure (s, e3, env2) -> interpret ((s, v2) :: env2) e3
        | _ -> failwith "Impossible"