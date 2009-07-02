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

// This file is a script that can be executed with the F# Interactive.  
// It can be used to explore and test the library project.
// Note that script files will not be part of the project build.

#load "Utils.fs"
#load "..\LambdaCalculus\ParserMonad.fs"
#load "..\LambdaCalculus\ParserCombinators.fs" 
#load "..\LambdaCalculus\Tokenizer.fs"
#load "..\LambdaCalculus\Parser.fs"
#load "..\LambdaCalculus\AstToCode.fs"
#load "..\LambdaCalculus\Interpreter.fs"


open Tokenizer
open Parser
open Interpreter
open AstToCode
open Utils

let fac = desugar "(λk.λi.((ifThenElse) (iszero i) 1 (k (pred i))))"

let interpret e = e
                  |> tokenize
                  |> parse
                  |> apply []
                  |> close []
                //  |> reduce
                  |> toString

interpret ("(λf.((λx.(f (λy.(x x y)))) (λx.(f (λy.(x x y)))))) " + fac) 

//interpret ("(λf.((λx.(f (λy.(x x y)))) (λx.(f (λy.(x x y)))))) (λx.(h x))")

//interpret ("(λf.((λx.(f (λy.(x x y)))) (λx.(f (λy.(x x y)))))) " + fac + " (λf.λx.x)")

//((λf.(λx.(f (x x))) (λx.(f (x x)))))

//interpret ("((λf.(λx.(f (x x))) (λx.(f (x x))))) " + fac + " (λf.λx.x)")