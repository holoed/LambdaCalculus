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
open Interpreter
open Utils
open AstToCode
open Numbers

let interpret' txt = txt |> desugar |> interpret
let interpret txt = txt |> interpret' |> toString

[<TestFixture>]
type InterpreterTests =
    new() = {}
    
    [<Test>]
    member o.NoOp() = 
        Assert.AreEqual("(λy.y)", interpret "(λy.y)")
    
    [<Test>]
    member o.Identity() = 
        Assert.AreEqual("(λy.y)", interpret "(λx.x) (λy.y)")
        
    [<Test>]
    member o.Succ() = 
        Assert.AreEqual("(λn.(λf.(λx.(f ((n f) x)))))", interpret "succ")
        
    [<Test>]
    member o.SuccOne() = 
        Assert.AreEqual("(λf.(λx.(f (f x))))", interpret "succ 1")

    [<Test>]
    member o.SuccTwo() = 
        Assert.AreEqual("(λf.(λx.(f (f (f x)))))", interpret "succ 2")           

    [<Test>]
    member o.SuccThree() = 
        Assert.AreEqual("(λf.(λx.(f (f (f (f x))))))", interpret "succ 3") 
 
    [<Test>] 
    member o.OnePlusZero() = 
        Assert.AreEqual("(λf.(λx.(f x)))", interpret "+ 1 0") 
                     
    [<Test>] 
    member o.OnePlusOne() = 
        Assert.AreEqual("(λf.(λx.(f (f x))))", interpret "+ 1 1") 
              
    [<Test>] 
    member o.TwoPlusOne() = 
        Assert.AreEqual("(λf.(λx.(f (f (f x)))))", interpret "+ 2 1")         
        
    [<Test>] 
    member o.ThreePlusTwo() = 
        Assert.AreEqual("(λf.(λx.(f (f (f (f (f x)))))))", interpret "+ 2 3")  
        
    [<Test>]
    member o.TwoMultiplyByThree() =
        Assert.AreEqual("(λf.(λx.(f (f (f (f (f (f x))))))))", interpret "* 2 3")     
        
    [<Test>]
    member o.Pred() =
        Assert.AreEqual("(λn.(λf.(λx.(((n (λg.(λh.(h (g f))))) (λu.x)) (λu.u)))))", interpret "pred")     
        
    [<Test>]
    member o.PredOne() =
        Assert.AreEqual("(λf.(λx.x))", interpret "pred 1")  

    [<Test>]
    member o.PredFour() =
        Assert.AreEqual("(λf.(λx.(f (f (f x)))))", interpret "pred 4")     

    [<Test>]
    member o.True() =
        Assert.AreEqual("(λx.(λy.x))", interpret "true")  
        
    [<Test>]
    member o.False() =
        Assert.AreEqual("(λx.(λy.y))", interpret "false")  
        
    [<Test>]
    member o.IsZero() =
        Assert.AreEqual("(λn.((n (λx.(λx.(λy.y)))) (λx.(λy.x))))", interpret "iszero")     

    [<Test>]
    member o.IsZeroAppliedToZeroIsTrue() =
        Assert.AreEqual("(λx.(λy.x))", interpret "iszero 0") 
        
    [<Test>]
    member o.IsZeroAppliedToOneIsFalse() =
        Assert.AreEqual("(λx.(λy.y))", interpret "iszero 1")    
        
    [<Test>]
    member o.IsZeroAppliedToThreeIsFalse() =
        Assert.AreEqual("(λx.(λy.y))", interpret "iszero 3")  
        
    [<Test>]
    member o.IfThenElse() =
        Assert.AreEqual("(λp.(λa.(λb.((p a) b))))", interpret "ifThenElse")    
        
    [<Test>]
    member o.IfTrueThenThree() =
         Assert.AreEqual("(λf.(λx.(f (f (f x)))))", interpret "(ifThenElse) (true) 3 5")       
        
    [<Test>]
    member o.IfFalseThenFive() =
         Assert.AreEqual("(λf.(λx.(f (f (f (f (f x)))))))", interpret "(ifThenElse) (false) 3 5") 
         
    [<Test>]
    member o.IfZeroOneElseNumberMinusOne() =
        Assert.AreEqual("(λf.(λx.(f x)))", interpret "(λi.((ifThenElse) (iszero i) 1 (pred i))) 0")   
        Assert.AreEqual("(λf.(λx.x))", interpret "(λi.((ifThenElse) (iszero i) 1 (pred i))) 1")   
        Assert.AreEqual("(λf.(λx.(f x)))", interpret "(λi.((ifThenElse) (iszero i) 1 (pred i))) 2")   
        Assert.AreEqual("(λf.(λx.(f (f x))))", interpret "(λi.((ifThenElse) (iszero i) 1 (pred i))) 3")    
        Assert.AreEqual("(λf.(λx.(f (f (f x)))))", interpret "(λi.((ifThenElse) (iszero i) 1 (pred i))) 4")
        
    [<Test>]
    member o.AlmostFactorial() =
        Assert.AreEqual("(λf.(λx.(f (f (f x)))))", interpret "(λk.λi.((ifThenElse) (iszero i) 1 (k (pred i)))) (λx.x) 4")
        
    [<Test>]    
    member o.AlmostFactorialFixed() =
        Assert.AreEqual("(λf.(λx.(f x)))", interpret "fix (λk.λi.((ifThenElse) (iszero i) 1 (k (pred i)))) 0")
        
    [<Test>]    
    member o.Factorial3() =
        Assert.AreEqual("(λf.(λx.(f (f (f (f (f (f x))))))))", interpret "fix (λk.λi.((ifThenElse) (iszero i) 1 (* i (k (pred i))))) 3")
        
    [<Test>]    
    member o.Factorial4() =
        let ret = "fix (λk.λi.((ifThenElse) (iszero i) 1 (* i (k (pred i))))) 4"
                  |> interpret'
                  |> toNumber
        Assert.AreEqual(24, ret)
       
    [<Test>]    
    member o.Factorial5() =
        let ret = "fix (λk.λi.((ifThenElse) (iszero i) 1 (* i (k (pred i))))) 5"
                  |> interpret'
                  |> toNumber
        Assert.AreEqual(120, ret)
        
    [<Test>] 
    member o.Factorial6() =
        let ret = "fix (λk.λi.((ifThenElse) (iszero i) 1 (* i (k (pred i))))) 6"
                  |> interpret'
                  |> toNumber
        Assert.AreEqual(720, ret)

    [<Test>] 
     [<Ignore("Works only in release mode")>]
    member o.Factorial7() =
        let ret = "fix (λk.λi.((ifThenElse) (iszero i) 1 (* i (k (pred i))))) 7"
                  |> interpret'
                  |> toNumber
        Assert.AreEqual(5040, ret)
        
    [<Test>] 
    [<Ignore("It works but it takes more than an hour.")>]
    member o.Factorial8() =
        let ret = "fix (λk.λi.((ifThenElse) (iszero i) 1 (* i (k (pred i))))) 8)"
                  |> interpret'
       
        Assert.AreEqual(5040, ret)
        
    [<Test>]
    member o.And() =
        Assert.AreEqual("(λx.(λy.x))", interpret "(and) (true) (true)")   
        Assert.AreEqual("(λx.(λy.y))", interpret "(and) (true) (false)")   
        Assert.AreEqual("(λx.(λy.y))", interpret "(and) (false) (true)") 
        Assert.AreEqual("(λx.(λy.y))", interpret "(and) (false) (false)")  
        
    [<Test>]
    member o.Or() =
        Assert.AreEqual("(λx.(λy.x))", interpret "(or) (true) (true)")   
        Assert.AreEqual("(λx.(λy.x))", interpret "(or) (true) (false)")   
        Assert.AreEqual("(λx.(λy.x))", interpret "(or) (false) (true)") 
        Assert.AreEqual("(λx.(λy.y))", interpret "(or) (false) (false)")     
        
    [<Test>]
    member o.Not() =
        Assert.AreEqual("(λa.(λb.b))", interpret "(not) (true)")  
        Assert.AreEqual("(λa.(λb.a))", interpret "(not) (false)")   
        
    [<Test>]
    member o.Pair() =
        let (x, y) = (5, 6)
        let pair = interpret (sprintf "pair %i %i" x y) 
        let first = interpret' (sprintf "first %s" pair)
        let second = interpret' (sprintf "second %s" pair)
        Assert.AreEqual(x, toNumber first)
        Assert.AreEqual(y, toNumber second)
        