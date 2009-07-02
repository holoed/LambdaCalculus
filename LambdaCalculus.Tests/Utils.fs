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

let succ = "(λn.λf.λx.(f (n f x)))"
let pred = "(λn.λf.λx.(n (λg.λh.(h (g f))) (λu.x) (λu.u)))"
let plus = "(λm.λn.λf.λx.((m f) (n f x)))"
let mult = "(λm.λn.λf.(n (m f)))"
let zero = "(λf.λx.x)"
let one  = "(λf.λx.(f x))"
let two  = "(λf.λx.(f (f x)))"
let three  = "(λf.λx.(f (f (f x))))"
let four   = "(λf.λx.(f (f (f (f x)))))"
let five   = "(λf.λx.(f (f (f (f (f x))))))"
let btrue  = "λx.λy.x"
let bfalse  = "λx.λy.y"
let ifthenelse = "λp.λa.λb.(p a b)"
let iszero = "(λn.(n (λx.(false)) (true)))"
let fix = "(λf.((λx.(f (λy.(x x y)))) (λx.(f (λy.(x x y))))))"

let desugar (txt:string) = txt  
                                .Replace("succ", succ)
                                .Replace("pred", pred)
                                .Replace("+", plus)
                                .Replace("*", mult)
                                .Replace("0", zero)
                                .Replace("1", one)
                                .Replace("2", two)
                                .Replace("3", three)
                                .Replace("4", four)
                                .Replace("5", five)
                                .Replace("ifThenElse", ifthenelse)
                                .Replace("iszero", iszero)
                                .Replace("true", btrue)
                                .Replace("false", bfalse)
                                .Replace("fix", fix)

