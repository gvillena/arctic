Imports CommandLine
Imports CommandLine.Text

Class Options

    <CommandLine.Option('r', "read", Required := true,
    HelpText:="Input files to be processed.")>
    Public Property InputFiles As IEnumerable(Of String)

    ' Omitting long name, default --verbose
    <CommandLine.Option(
    HelpText:="Prints all messages to standard output.")>
    Public Property Verbose As Boolean

    <CommandLine.Option(Default:="中文",
    HelpText:="Content language.")>
    Public Property Language As String

    <CommandLine.Value(0, MetaName:="offset",
    HelpText:="File offset.")>
    Public Property Offset As Long?

End Class

<CommandLine.Verb("run", HelpText:="Run some app.")>
Class RunOptions
End Class

<CommandLine.Verb("pweb162", HelpText:="TODO")>
Class Pweb162Options
End Class

<CommandLine.Verb("sim162", HelpText:="TODO")>
Class Sim162Options
End Class

<CommandLine.Verb("so162", HelpText:="TODO")>
Class So162Options
End Class

<CommandLine.Verb("visual162", HelpText:="TODO")>
Class Visual162Options
End Class

<CommandLine.Verb("list", HelpText:="List all students.")>
Class ListOptions
End Class

<CommandLine.Verb("grade", HelpText:="Calculate your grade.")>
Class GradeOptions
End Class

<CommandLine.Verb("test", HelpText:="My test command.")>
Class TestOptions
End Class

