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

module ContinuationMonad


type ContinuationMonadBuilder() =
    // Ma -> (a -> Mb) -> Mb
    member this.Bind (m, f) = fun b -> m(fun a -> f a b)
    //a -> Ma
    member this.Return x = fun f -> f x
    

let cont = ContinuationMonadBuilder()