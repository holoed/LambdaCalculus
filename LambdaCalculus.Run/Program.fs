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
open Tokenizer
open Parser
open Interpreter
open AstToCode

printfn "Lambda Calculus interpreter 0.0.0.0"

System.Console.OutputEncoding <- System.Text.Encoding.UTF8

let rec readLine() = let ch = System.Console.ReadKey();
                     let pos = if Console.CursorLeft > 0 then (Console.CursorLeft - 1) else 0
                     System.Console.SetCursorPosition(pos, Console.CursorTop);
                     if (ch.KeyChar = '\b') then
                      System.Console.SetCursorPosition(pos + 1, Console.CursorTop);
                      Console.Write(' ');
                     if (ch.KeyChar = '\r') then
                      Console.WriteLine()
                      sprintf "%c" ch.KeyChar
                     elif (ch.KeyChar = '\\') then
                      Console.Write('λ')
                      sprintf "%c%s" 'λ' (readLine()) 
                     else
                      Console.Write(ch.KeyChar);
                      sprintf "%c%s" ch.KeyChar (readLine()) 
               

while(true) do
 printf "> "
 let line = readLine()
 if (line <> null && line <> "" && line<> "\r") then
        try
            printfn "%s" (interpret line)
        with
        | ex -> printfn "%s" ex.Message
                         