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

//A parser which successfully consumes the first character
//if the argument string is non-empty, and fails otherwise.
let item = Parser (fun cs -> match cs with
                             | [] -> []
                             | x::xs -> [x, xs])
                            
//A combinator sat that takes a predicate, and yields a parser that
//consumes a single character if it satisfies the predicate, and fails otherwise.
let sat p = parser { let! c = item
                     if p c then
                      return c }
                                                                                                                           
//The many combinator permits zero
//or more applications of p, while many1 permits one or more.
let rec many1 p = parser { let! x = p
                           let! xs = many p
                           return (x::xs) }
and many p = (many1 p) +++ parser { return [] }

//Parse repeated applications of a parser p, separated by applications of a parser
//op whose result value is an operator that is assumed to associate to the left,
//and which is used to combine the results from the p parsers.
and chainl1 p op = 
    let rec rest a = parser { let! f = op
                              let! b = p
                              return! rest (f a b) } +++ parser { return a }
    parser { let! a = p
             return! rest a } 

//Apply a parser p
let apply p = extract p