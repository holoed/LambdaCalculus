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
open Tokenizer
open AstToCode

let rec subst x v a =
  match a with 
  | Var y -> 
        if x = y then v else a
  | Lambda(y, a') ->
        if x = y then a else Lambda(y, subst x v a')
  | Apply(a', a'') ->
        Apply(subst x v a', subst x v a'')

let rec reduce e =
    //printfn "%s" (toString e)
    let rec reduce' e = 
        match e with
        | Var _ -> e
        | Lambda (s, e') -> Lambda(s, reduce' e')
        | Apply(e1, e2) ->
           match e1 with
           | Lambda(s, e3) -> subst s e2 e3
           | _ -> Apply(reduce' e1, reduce' e2)
    reduce' e
    
let rec loop f x =
    let x' = f x
    if x = x' then x' else loop f x'

let interpret e = e
                  |> tokenize
                  |> parse
                  |> loop reduce
