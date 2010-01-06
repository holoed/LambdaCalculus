namespace LambdaCalculus
module IO = 

    type IO<'a> = IOAction of (unit -> 'a)
    
    type IOMonad () =
        // m a -> (a -> m b) -> m b
        member o.Bind (m : IO<'a>, f:'a -> IO<'b>) = let (IOAction g) = m in f ( g () )
        // a -> m a
        member o.Return x = IOAction (fun () -> x)
        // m a -> m a
        member o.ReturnFrom m = m

    let IO = IOMonad ()

    let run (IOAction f) = f ()

    open System

    type Helpers =
        static member cursorLeft = IOAction(fun () -> Console.CursorLeft)
        static member cursorTop  = IOAction(fun () -> Console.CursorTop)
        static member readKey () = IOAction(fun () -> Console.ReadKey () )
        static member write (ch:char) = IOAction(fun () -> Console.Write ch )
        static member write (ch:string) = IOAction(fun () -> Console.Write ch )
        static member writeLine () = IOAction(fun () -> Console.WriteLine () )
        static member setCursorPosition left top = IOAction(fun () -> Console.SetCursorPosition (left, top))

