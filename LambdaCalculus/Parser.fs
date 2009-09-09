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

open ParserMonad
open ParserCombinators
open Tokenizer
open Ast

let isWhiteSpace = function
                   | WhiteSpace -> true
                   | _ -> false

let isLetter = function
               | Letter(_) -> true
               | _ -> false
               
let isSymbol x = function
                 | Symbol(x') when x = x' -> true
                 | _ -> false
            
let var = parser { let! x = sat isLetter
                   return Var(x) }
                   
let appOp = parser { let! _ = sat isWhiteSpace
                     return fun x y -> Apply(x,y) }
                   
let rec exp = chainl1 term appOp
     and term = lambda ++ var +++ parser { let! _ = sat (isSymbol('('))
                                           let! n = exp
                                           let! _ = sat (isSymbol(')'))
                                           return n }
     and lambda = parser { let! _ = sat (isSymbol('λ'))
                           let! arg = sat (isLetter)
                           let! _ = sat (isSymbol('.'))
                           let! body = exp
                           return  Lambda(arg, body) }                   

let parse ts = match apply exp ts  with
               | [] -> failwith "failed to parse"
               | (ret, _)::xs -> ret 
