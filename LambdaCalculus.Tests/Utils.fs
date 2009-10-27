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

namespace LambdaCalculus.Tests
module Utils = 

    open LambdaCalculus.Numbers
    open LambdaCalculus.AstToCode

    let succ = "(λn.λf.λx.(f (n f x)))"
    let pred = "(λn.λf.λx.(n (λg.λh.(h (g f))) (λu.x) (λu.u)))"
    let plus = "(λm.λn.λf.λx.((m f) (n f x)))"
    let mult = "(λm.λn.λf.(n (m f)))"
    let btrue  = "λx.λy.x"
    let bfalse  = "λx.λy.y"
    let bAnd = "λp.λq.(p q p)"
    let bOr = "λp.λq.(p p q)"
    let not = "λp.λa.λb.(p b a)"
    let ifthenelse = "λp.λa.λb.(p a b)"
    let iszero = "(λn.(n (λx.(false)) (true)))"
    let fix = "(λf.((λx.(f (λy.(x x y)))) (λx.(f (λy.(x x y))))))"

    // Pairs (2-Tuple)
    let pair = "(λx.λy.λf.(f x y))"
    let first = "(λp.(p true))"
    let second = "(λp.(p false))"

    let FromNumber = FromNumber >> toString


    let desugar (txt:string) = txt  
                                    .Replace("pair", pair)
                                    .Replace("first", first)
                                    .Replace("second", second)
                                    .Replace("succ", succ)
                                    .Replace("pred", pred)
                                    .Replace("+", plus)
                                    .Replace("*", mult)
                                    .Replace("0", FromNumber 0)
                                    .Replace("1", FromNumber 1)
                                    .Replace("2", FromNumber 2)
                                    .Replace("3", FromNumber 3)
                                    .Replace("4", FromNumber 4)
                                    .Replace("5", FromNumber 5)
                                    .Replace("6", FromNumber 6)
                                    .Replace("7", FromNumber 7)
                                    .Replace("ifThenElse", ifthenelse)
                                    .Replace("iszero", iszero)
                                    .Replace("and", bAnd)
                                    .Replace("or", bOr)
                                    .Replace("not", not)
                                    .Replace("true", btrue)
                                    .Replace("false", bfalse)
                                    .Replace("fix", fix)

