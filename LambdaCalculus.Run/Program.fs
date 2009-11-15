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

namespace LambdaCalculus.Run
module Program =

    open System
    open LambdaCalculus.Tokenizer
    open LambdaCalculus.Parser
    open LambdaCalculus.Interpreter
    open LambdaCalculus.AstToCode
    open LambdaCalculus.Numbers
    open LambdaCalculus.Repl
    open LambdaCalculus.IO

    printfn "Lambda Calculus interpreter 0.0.0.1"

    System.Console.OutputEncoding <- System.Text.Encoding.UTF8
                             
    while(true) do run (IO { 
                              do! Helpers.write "> "
                              let! line = readLine ""
                              return if (line <> null && line <> "" && line<> "\r") then
                                        try
                                            line
                                            |> interpret
                                            |> toString
                                            |> printfn "%s"
                                        with
                                        | ex -> printfn "%s" ex.Message })
                                             