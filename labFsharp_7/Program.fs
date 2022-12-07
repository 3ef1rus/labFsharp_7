let number = "5"
let answerAgent = 
    MailboxProcessor.Start(fun inbox->
        let rec messageLoop() =
            async{
            let! msg = inbox.Receive()
            if msg = number then printfn "%s - ДА" msg
            else printfn "%s - НЕТ" msg
            return! messageLoop()
            }
        messageLoop()
     )

let printerAgent = MailboxProcessor.Start(fun inbox->
 // обработка сообщений
 let rec messageLoop() = async{
 // чтение сообщения
    let! msg = inbox.Receive()
 // печать сообщения
    printfn "Сообщение: %s" msg
    return! messageLoop()
 }
 // запуск обработки сообщений
 messageLoop()
 )

printerAgent.Post "Сообщение 1"
printerAgent.Post "Сообщение 2"
answerAgent.Post "5"
for i in [1..10] do
    answerAgent.Post(i.ToString())

    
    
