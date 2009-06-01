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

open System
open ParserMonad
open ParserCombinators

type Token = | Letter of char
             | Symbol of char
             | WhiteSpace

let letter = parser { let! x = sat Char.IsLetter
                      return Letter(x) }
                       
let symbol = parser { let! x = sat (fun ch -> ch = 'λ' or ch = '.' or ch = '(' or ch = ')')
                      return Symbol(x) } 
                      
let whiteSpace = parser { let! x = sat (fun ch -> ch = ' ')
                          return WhiteSpace }                   

let lex = symbol +++ letter +++ whiteSpace

let lex' = parser { let! x = many lex
                    return x }

let tokenize s = match apply lex' (Seq.to_list s)  with
                 | [] -> failwith "failed to tokenize"
                 | (ret, _)::xs -> ret 
