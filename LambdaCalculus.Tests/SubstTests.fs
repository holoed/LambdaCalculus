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

open NUnit.Framework
open Tokenizer
open Ast
open Parser
open Interpreter
open Numbers
open AstToCode

let parse exp = exp |> tokenize |> parse

[<TestFixture>]
type SubstTests=
    new () = {}
    
    [<Test>]
    member o.SubsOne() = 
        Assert.AreEqual(Var (Letter 'y'), 
            subst (Letter 'x') (Var(Letter 'y')) (Var (Letter 'x'))) 
        
    [<Test>]
    member o.DontSubs() = 
        Assert.AreEqual(Var (Letter 'x'), 
            subst (Letter 'z') (Var(Letter 'y')) (Var (Letter 'x'))) 
        
    [<Test>]
    member o.LambdaSubs() = 
        Assert.AreEqual((Lambda(Letter 'a', Var (Letter 'y'))), 
            subst (Letter 'x') (Var(Letter 'y')) (Lambda(Letter 'a', Var (Letter 'x')))) 
            
    [<Test>]
    member o.ApplySubs() = 
        Assert.AreEqual(
            parse "λx.(g (λy.k))", 
            subst (Letter 'c') (Var(Letter 'k')) (parse "λx.(g (λy.c))")) 
            
    [<Test>]
    member o.Test5() = 
        Assert.AreEqual(
            parse "λx.((λj.k) (λy.k))", 
            subst (Letter 'c') (Var(Letter 'k')) (parse "λx.((λj.c) (λy.c))")) 
            
    [<Test>]
    member o.Test6() = 
        Assert.AreEqual(
            parse "λx.((λa.k) (λb.((λe.k) (λf.k))))", 
            subst (Letter 'c') (Var(Letter 'k')) (parse "λx.((λa.c) (λb.((λe.c) (λf.c))))"))             

    [<Test>]
    member o.Test7() = 
        Assert.AreEqual(
            parse "λx.((λa.k) (λc.((λe.c) (λf.c))))", 
            subst (Letter 'c') (Var(Letter 'k')) (parse "λx.((λa.c) (λc.((λe.c) (λf.c))))"))   
            
            
    [<Test>]
    [<Ignore("Make toNumber Tail Recursive")>]
    member o.Test8() = 
        let number = match (FromNumber 100000) with
                     | Lambda(Letter 'f', body) -> body 
                     | _ -> failwith "not a number"
        
        let ret = number
                  |> subst (Letter 'f') (Var(Letter 'g'))
                  |> subst (Letter 'g') (Var(Letter 'f'))
                  |> toNumber
        Assert.AreEqual(100000, ret)