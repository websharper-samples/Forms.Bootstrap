namespace LoginWithWebSharperForms

open WebSharper
open WebSharper.UI
open WebSharper.UI.Html

[<JavaScript>]
module Client =
    open WebSharper.JavaScript
    open WebSharper.UI.Client
    open WebSharper.Forms
    open WebSharper.Forms.Bootstrap

    [<SPAEntryPoint>]
    let LoginForm() =
        Form.Return (fun user pass check -> user, pass, check)
        <*> (Form.Yield ""
            |> Validation.IsNotEmpty "Must enter a username")
        <*> (Form.Yield ""
            |> Validation.IsNotEmpty "Must enter a password")
        <*> Form.Yield false
        |> Form.WithSubmit
        |> Form.Run (fun (user, pass, check) ->
            JS.Alert("Welcome, " + user + "!")
        )
        |> Form.Render (fun user pass check submit ->
            form [] [
                Controls.Simple.InputWithError "Username" user submit.View
                Controls.Simple.InputPasswordWithError "Password" pass submit.View
                Controls.Simple.Checkbox "Keep me logged in" check
                Controls.Button "Log in" [attr.``class`` "btn btn-primary"] submit.Trigger
                Controls.ShowErrors [attr.style "margin-top:1em;"] submit.View
            ]
        )
        |> fun s -> s.RunById "main"
