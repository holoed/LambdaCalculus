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
        Assert.AreEqual("(λn.(λf.(λx.(f ((n f) x)))))", interpret "(λn.λf.λx.(f (n f x)))")
        
    [<Test>]
    member o.SuccOne() = 
        Assert.AreEqual("(λf.(λx.(f (f x))))", interpret "(λn.λf.λx.(f (n f x))) (λf.λx.(f x))")

    [<Test>]
    member o.SuccTwo() = 
        Assert.AreEqual("(λf.(λx.(f (f (f x)))))", interpret "(λn.λf.λx.(f (n f x))) (λf.λx.(f (f x)))")           

    [<Test>]
    member o.SuccThree() = 
        Assert.AreEqual("(λf.(λx.(f (f (f (f x))))))", interpret "(λn.λf.λx.(f (n f x))) (λf.λx.(f (f (f x))))") 
 
    [<Test>] 
    member o.OnePlusZero() = 
        Assert.AreEqual("(λf.(λx.(f x)))", interpret "(λm.λn.λf.λx.((m f) (n f x))) (λf.λx.(f x)) (λf.λx.x)") 
                     
    [<Test>] 
    member o.OnePlusOne() = 
        Assert.AreEqual("(λf.(λx.(f (f x))))", interpret "(λm.λn.λf.λx.((m f) (n f x))) (λf.λx.(f x)) (λf.λx.(f x))") 
              
    [<Test>] 
    member o.TwoPlusOne() = 
        Assert.AreEqual("(λf.(λx.(f (f (f x)))))", interpret "(λm.λn.λf.λx.((m f) (n f x))) (λf.λx.(f (f x))) (λf.λx.(f x))")         
        
    [<Test>] 
    member o.ThreePlusTwo() = 
        Assert.AreEqual("(λf.(λx.(f (f (f (f (f x)))))))", interpret "(λm.λn.λf.λx.((m f) (n f x))) (λh.λx.(h (h x))) (λg.λx.(g (g (g x))))")  
        
    [<Test>]
    member o.TwoMultiplyByThree() =
        Assert.AreEqual("(λf.(λx.(f (f (f (f (f (f x))))))))", interpret "(λm.λn.λf.(n (m f))) (λh.λx.(h (h x))) (λg.λx.(g (g (g x))))")     
        
    [<Test>]
    member o.Pred() =
        Assert.AreEqual("(λn.(λf.(λx.(((n (λg.(λh.(h (g f))))) (λu.x)) (λu.u)))))", interpret "(λn.λf.λx.(n (λg.λh.(h (g f))) (λu.x) (λu.u)))")     
        
    [<Test>]
    member o.PredFour() =
        Assert.AreEqual("(λf.(λx.(f (f (f x)))))", interpret "(λn.λf.λx.(n (λg.λh.(h (g f))) (λu.x) (λu.u))) (λf.λx.(f (f (f (f x)))))")     

    [<Test>]
    member o.True() =
        Assert.AreEqual("(λx.(λy.x))", interpret "λx.λy.x")  
        
    [<Test>]
    member o.False() =
        Assert.AreEqual("(λx.(λy.y))", interpret "λx.λy.y")  
        
    [<Test>]
    member o.IsZero() =
        Assert.AreEqual("(λn.((n (λx.(λx.(λy.y)))) (λx.(λy.x))))", interpret "λn.(n (λx.(λx.λy.y)) (λx.λy.x))")     

    [<Test>]
    member o.IsZeroAppliedToZeroIsTrue() =
        Assert.AreEqual("(λx.(λy.x))", interpret "(λn.(n (λx.(λx.λy.y)) (λx.λy.x))) (λf.λx.x)") 
        
    [<Test>]
    member o.IsZeroAppliedToOneIsFalse() =
        Assert.AreEqual("(λx.(λy.y))", interpret "(λn.(n (λx.(λx.λy.y)) (λx.λy.x))) (λf.λx.(f x))")    
        
    [<Test>]
    member o.IsZeroAppliedToThreeIsFalse() =
        Assert.AreEqual("(λx.(λy.y))", interpret "(λn.(n (λx.(λx.λy.y)) (λx.λy.x))) (λf.λx.(f (f (f x))))")  
        
    [<Test>]
    member o.IfThenElse() =
        Assert.AreEqual("(λp.(λa.(λb.((p a) b))))", interpret "λp.λa.λb.(p a b)")    
        
    [<Test>]
    member o.IfTrueThenThree() =
         Assert.AreEqual("(λf.(λx.(f (f (f x)))))", interpret "(λp.λa.λb.(p a b)) (λx.λy.x) (λf.λx.(f (f (f x)))) (λf.λx.(f (f (f (f (f x))))))")       
        
    [<Test>]
    member o.IfFalseThenFive() =
         Assert.AreEqual("(λf.(λx.(f (f (f (f (f x)))))))", interpret "(λp.λa.λb.(p a b)) (λx.λy.y) (λf.λx.(f (f (f x)))) (λf.λx.(f (f (f (f (f x))))))") 
         
    [<Test>]
    member o.IfZeroOneElseNumberMinusOne() =
        Assert.AreEqual("(λf.(λx.(f x)))", interpret "(λi.((λp.λa.λb.(p a b)) ((λn.(n (λx.(λx.λy.y)) (λx.λy.x))) i) (λf.λx.(f x)) ((λn.λf.λx.(n (λg.λh.(h (g f))) (λu.x) (λu.u))) i))) (λf.λx.x)")   
        Assert.AreEqual("(λf.(λx.x))", interpret "(λi.((λp.λa.λb.(p a b)) ((λn.(n (λx.(λx.λy.y)) (λx.λy.x))) i) (λf.λx.(f x)) ((λn.λf.λx.(n (λg.λh.(h (g f))) (λu.x) (λu.u))) i))) (λf.λx.(f x))")   
        Assert.AreEqual("(λf.(λx.(f x)))", interpret "(λi.((λp.λa.λb.(p a b)) ((λn.(n (λx.(λx.λy.y)) (λx.λy.x))) i) (λf.λx.(f x)) ((λn.λf.λx.(n (λg.λh.(h (g f))) (λu.x) (λu.u))) i))) (λf.λx.(f (f x)))")   
        Assert.AreEqual("(λf.(λx.(f (f x))))", interpret "(λi.((λp.λa.λb.(p a b)) ((λn.(n (λx.(λx.λy.y)) (λx.λy.x))) i) (λf.λx.(f x)) ((λn.λf.λx.(n (λg.λh.(h (g f))) (λu.x) (λu.u))) i))) (λf.λx.(f (f (f x))))")    
        Assert.AreEqual("(λf.(λx.(f (f (f x)))))", interpret "(λi.((λp.λa.λb.(p a b)) ((λn.(n (λx.(λx.λy.y)) (λx.λy.x))) i) (λf.λx.(f x)) ((λn.λf.λx.(n (λg.λh.(h (g f))) (λu.x) (λu.u))) i))) (λf.λx.(f (f (f (f x)))))")
        
    [<Test>]
    member o.AlmostFactorial() =
        Assert.AreEqual("(λf.(λx.(f (f (f x)))))", interpret "(λk.λi.((λp.λa.λb.(p a b)) ((λn.(n (λx.(λx.λy.y)) (λx.λy.x))) i) (λf.λx.(f x)) (k ((λn.λf.λx.(n (λg.λh.(h (g f))) (λu.x) (λu.u))) i)))) (λx.x) (λf.λx.(f (f (f (f x)))))")