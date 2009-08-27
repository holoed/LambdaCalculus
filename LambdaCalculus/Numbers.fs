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

let rec ToNumber exp = match exp with
                       | Lambda(f, Lambda(x, Var x')) when x = x' -> 0
                       | Lambda(f, Lambda(x, Apply(Var f', exp''))) when f = f' -> 
                                1 + ToNumber (Lambda(f, Lambda(x, exp'')))
                       | _ -> failwith "NaN"

//Tail Recursive Version using CPS Technique                         
let FromNumber n = 
    let rec Loop n cont = 
        match n with 
        | 0 -> cont (Var (Letter 'x')) 
        | n -> Loop (n - 1) (fun nacc ->  
                       cont (Apply(Var (Letter 'f'), nacc)))
    Lambda(Letter 'f', Lambda(Letter 'x', Loop n (fun x -> x))) 
                       
//let FromNumber exp = 
//                 let rec FromNumberAux exp = 
//                         match exp with
//                         | 0 -> Var (Letter 'x')
//                         | n -> Apply(Var (Letter 'f'), FromNumberAux (n - 1))
//                 Lambda(Letter 'f', Lambda(Letter 'x', FromNumberAux exp)) 

