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

// Parser Monad (http://www.cs.nott.ac.uk/~gmh/pearl.pdf)

namespace LambdaCalculus
module ParserMonad = 

    type Parser<'a,'b> = Parser of ('a ->('b * 'a) list)

    let extract(Parser f) = f

    type ParserMonad() =
        member b.Bind (p, f) = Parser (fun cs ->
                                                let r = extract p cs in
                                                let r' = List.map (fun (a,cs') -> extract (f a) cs') r in
                                                List.concat r')
        member b.Return x = Parser (fun cs -> [x,cs])
        member b.Zero () = Parser (fun cs -> [])
        member b.ReturnFrom x = x


    let (++) p q = Parser(fun cs -> List.append (extract p cs) (extract q cs))
    let (+++) p q = Parser(fun cs -> match (extract(p ++ q) cs) with
                                     | [] -> []
                                     | x::xs -> [x])

    let parser = ParserMonad()