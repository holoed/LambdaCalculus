namespace LambdaCalculus
module Repl = 
    open LambdaCalculus.IO
    
    let private handleBackspace (s:string) pos  = IO {
                                  let! top = Helpers.cursorTop
                                  do! Helpers.setCursorPosition (pos + 1) top
                                  do! Helpers.write(' ')
                                  let! top = Helpers.cursorTop
                                  do! Helpers.setCursorPosition (pos + 1) top
                                  return s.Remove(s.Length - 1, 1) }

    let private handleLeftArrow (s:string) pos  = IO {
                                  let! top = Helpers.cursorTop
                                  do!
                                    if ((pos - 2) - s.Length) = 0 then
                                        Helpers.write(" ")
                                    else                                        
                                        Helpers.write(s.Chars (pos - 2))
                                  do! Helpers.setCursorPosition (pos - 1) top                                  
                                  return s }

    let private handleLambda s = IO {
                                  do! Helpers.write('λ')
                                  return sprintf "%s%c" s 'λ' }

    let private handleReturn s = IO {
                                    do! Helpers.writeLine()
                                    return s }

    let rec readLine (s:string) = IO {    
                                         let! ch = Helpers.readKey ()
                                         let! left = Helpers.cursorLeft                     
                                         let! top = Helpers.cursorTop             
                                         let pos = if left > 0 then (left - 1) else 0
                                         do! Helpers.setCursorPosition pos top
                                         match (ch.KeyChar, ch.Key, pos) with
                                         | ('\b', _, x) when x > 0 -> let! s' = handleBackspace s pos
                                                                      return! readLine s'
                                         | ('\r', _, _)            -> return! handleReturn s
                                         | ('\\', _, _)            -> let! s' = handleLambda s
                                                                      return! readLine s' 
                                         | (_, System.ConsoleKey.LeftArrow, _) -> let! s' = handleLeftArrow s pos
                                                                                  return! readLine s'
                                         | _                    -> do! Helpers.write(ch.KeyChar);
                                                                   return! readLine (sprintf "%s%c" s ch.KeyChar)  
                            }

