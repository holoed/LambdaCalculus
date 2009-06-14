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

#load "ParserMonad.fs"
#load "ParserCombinators.fs"
#load "Tokenizer.fs"
#load "Parser.fs"
#load "Interpreter.fs"
#load "AstToCode.fs"

open Tokenizer
open Parser
open Interpreter
open AstToCode

let f x = x
          |> tokenize
          |> parse
          |> interpret []
          |> toString
          
//f "?f.?x.(f x)"
//f "?f.?x.(f (f x))"          
f "(?n.?f.?x.(f (n f x))) (?f.?x.(f x))"