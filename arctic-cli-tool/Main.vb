Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports CommandLine
Imports CommandLine.Text
Imports Newtonsoft.Json

Module Main

    Sub Main()

        ' Declare and initialize parser 
        Dim parser = New Parser(Sub(config) config.HelpWriter = Console.Out)

        ' Parse result
        Dim args = My.Application.CommandLineArgs
        Dim result = parser.ParseArguments(Of Pweb162Options,
                                              Sim162Options,
                                              So162Options,
                                              Visual162Options,
                                              TestOptions)(args)

        ' Execute parsed action
        result.WithParsed(Of Pweb162Options)(AddressOf Pweb162).
                WithParsed(Of Sim162Options)(AddressOf Sim162).
                WithParsed(Of So162Options)(AddressOf So162).
                WithParsed(Of Visual162Options)(AddressOf Visual162).
                WithParsed(Of TestOptions)(AddressOf Test).
                WithNotParsed(Function(errors) 1)

    End Sub

    Function Pweb162(opts As Pweb162Options)
        Return 0
    End Function

    Function Sim162(opts As Sim162Options)

        Dim opc As String = String.Empty

        Console.WriteLine("-----------------------------")
        Console.WriteLine("Simulacion de Sistemas 2016-2")
        Console.WriteLine("-----------------------------")
        Console.WriteLine()
        Console.WriteLine("[1] Lista")
        Console.WriteLine("[2] Notas P1")
        Console.WriteLine("[3] Notas EP")
        Console.WriteLine("[4] Notas P2")
        Console.WriteLine("[5] Notas EF")
        Console.WriteLine("[6] Notas P1 / EP / P2 / EF")
        Console.WriteLine("[7] Promedios Finales")
        Console.WriteLine("[8] Promedios Finales (Notas)")
        Console.WriteLine("[9] Promedios Finales (Notas / Susti)")
        Console.WriteLine("[10] Salir")

        Console.WriteLine()
        Console.Write("Seleccione una opcion: ")
        opc = Console.ReadLine()

        Select Case opc

            ' Mostrar Lista
            Case "1"

                Dim sb = New StringBuilder()
                Dim stdlst = StudentIO.GetStudentsJson("sim162-lst-json.txt")

                sb.AppendFormat(" {0,-10}  {1, -40} {2}", "CODIGO", "APELLIDOS Y NOMBRES", vbCrLf)
                For Each std As Student In stdlst
                    sb.AppendFormat(" {0,-10}  {1, -40} {2}", std.Id, std.Name, vbCrLf)
                Next
                Console.WriteLine()
                Console.WriteLine(sb.ToString())

            Case "2"

                Console.WriteLine("NOTAS PRACTICA 01")
                Console.WriteLine()
                Console.WriteLine("[1] Mostrar")
                Console.WriteLine("[2] Ingresar")
                Console.WriteLine("[3] Modificar")
                Console.WriteLine("[4] Salir")
                Console.WriteLine()
                Console.Write("Seleccione una opcion: ")
                opc = Console.ReadLine()

                Select Case opc
                    Case 1
                        Dim sb = New StringBuilder()
                        Dim stdlst = StudentIO.GetStudentsJson("sim162-lst-json.txt")
                        sb.AppendFormat(" {0,-10}  {1, -40} {2:00.00} {3}", "CODIGO", "APELLIDOS Y NOMBRES", "P1", vbCrLf)

                        For Each std As Student In stdlst
                            sb.AppendFormat(" {0,-10}  {1, -40} {2:00.00} {3}", std.Id, std.Name,
                                                                                std.Grades(Evaluation.P1),
                                                                                vbCrLf)
                        Next
                        Console.WriteLine()
                        Console.WriteLine(sb.ToString())

                    Case 2

                        Console.WriteLine("INGRESAR NOTAS PRACTICA 01")
                        Console.WriteLine("--------------------------")
                        Console.WriteLine("MODOS DE INGRESO:")
                        Console.WriteLine()
                        Console.WriteLine(" [1] Normal")
                        Console.WriteLine(" [2] SmartEval")
                        Console.WriteLine(" [3] Salir")
                        Console.WriteLine()
                        Console.Write("Seleccione un modo de ingreso: ")
                        opc = Console.ReadLine()

                        Select Case opc
                            Case 1
                                Console.Write("Ingrese codigo de alumno o presione 'Enter' para todos los alumnos: ")
                                If String.IsNullOrEmpty(opc) Then

                                Else
                                    ' TODO Student One By One
                                End If
                            Case 2

                            Case Else

                        End Select



                    Case Else
                        Console.WriteLine("Opcion no valida... :(")
                End Select

                'Dim sb = New StringBuilder()
                'Dim stdlst = StudentIO.GetStudentsJson("sim162-lst-json.txt")
                'sb.AppendFormat(" {0,-10}  {1, -40} {2:00.00} {3:00.00} {4:00.00} {5:00.00} {6}", "CODIGO",
                '                                                                                  "APELLIDOS Y NOMBRES",
                '                                                                                  "P1", "EP", "P2", "EF",
                '                                                                                  vbCrLf)
                'For Each std As Student In stdlst
                '    sb.AppendFormat(" {0,-10}  {1, -40} {2:00.00} {3:00.00} {4:00.00} {5:00.00} {6}", std.Id,
                '                                                                                      std.Name,
                '                                                                                      std.Grades(Evaluation.P1),
                '                                                                                      std.Grades(Evaluation.EP),
                '                                                                                      std.Grades(Evaluation.P2),
                '                                                                                      std.Grades(Evaluation.EF),
                '                                                                                      vbCrLf)
                'Next
                'Console.WriteLine()
                'Console.WriteLine(sb.ToString())

            Case "3"
                Console.WriteLine("TODO")
            Case "4"
                Console.WriteLine("TODO")
            Case Else
                Console.WriteLine("Opcion no valida... :(")
        End Select
        Return 0
    End Function

    Function So162(opts As So162Options)
        Return 0
    End Function

    Function Visual162(opts As Visual162Options)
        Return 0
    End Function

    Function List(opts As ListOptions)

        Dim sb = New StringBuilder()
        Dim stdLst = StudentIO.GetStudents("sim162-lst.txt")

        sb.AppendLine()
        sb.AppendLine(" " & HeadingInfo.Default.ToString)
        sb.AppendLine()

        sb.AppendFormat("  {0,-10}  {1, -40} {2}", "Id", "Name", vbCrLf)

        For Each std As Student In stdLst
            sb.AppendFormat("  {0,-10}  {1, -40} {2}", std.Id, std.Name, vbCrLf)
        Next

        Console.WriteLine()
        Console.WriteLine(sb.ToString())
        Return 0
    End Function

    Function Grade(opts As GradeOptions)

        Console.WriteLine()
        Console.WriteLine("Calculate Grade Test v0.2")
        Console.WriteLine("-------------------------")
        Console.WriteLine()

        Console.Write("Codigo Alumno: ")
        Dim id = Console.ReadLine()

        Dim lstFileName = "sim162-lst.txt"
        Dim std = StudentIO.GetStudent(id, lstFileName)
        If std Is Nothing Then
            Console.WriteLine()
            Console.WriteLine("Opss :( no pude encontrar el codigo " & id & " en la lista " & lstFileName)
            Console.WriteLine("Verifica que todo este bien e intentalo de nuevo... ;)")
            Return 0
        End If

        Console.WriteLine(std.Name & Environment.NewLine)

        Dim grde = StudentIO.CalcGrade(Course.sim162, Evaluation.P1)
        Console.WriteLine()
        Console.WriteLine("¡Finalizado!")
        Console.WriteLine()
        Console.WriteLine()
        Console.WriteLine("Nota P1: " & grde.ToString("#0.00"))

        If grde > 10.5 Then
            Console.WriteLine("Aprobado ;)")
        Else

            Dim msgs = New ArrayList()
            With msgs
                .Add("Lo importante es que tienes salud ;)")
                .Add("Falta poco para verano ;)")
                .Add("¡A nada! ;)")
                .Add("¡Susti! ;)")
            End With

            Console.WriteLine("Sorry :(")
            Console.WriteLine(msgs(New Random().Next(3)))

        End If


        Return 0
    End Function

    Function Test(opts As TestOptions)

        Console.WriteLine()
        Console.WriteLine("¡Hello World!")
        Console.WriteLine()

        Pweb162M.InitGradeMngr()

        'Console.WriteLine("-----------------------------")
        'Console.WriteLine("- INGRESO NOTA EXAMEN FINAL -")
        'Console.WriteLine("-   SMART EVALUATION MODE   -")
        'Console.WriteLine("-----------------------------")
        'Console.WriteLine()
        'Console.WriteLine("HOLA")

        'Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        'Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "sim162-lst-json.txt")
        'Dim stdlst = StudentIO.GetStudentsFromJson("sim162-lst-json.txt")
        'Dim pdata = StudentIO.GetQuestionData(Evaluation.EF)
        'Dim tlst As New List(Of Integer)

        'Dim optn As String
        'Dim isValidOption = Function(x)
        '                        If Not Regex.IsMatch(x, "^[123]{1,1}$") Then
        '                            Console.WriteLine("OPCION NO VALIDA, INTENTALO DE NUEVO.")
        '                            Console.WriteLine()
        '                            Return False
        '                        End If
        '                        Return True
        '                    End Function

        'Do
        '    Console.WriteLine("MODO DE INGRESO")
        '    Console.WriteLine()
        '    Console.WriteLine(" [1] TODOS LOS EXAMENES POR LISTA DESDE EL PRINCIPIO.")
        '    Console.WriteLine(" [2] TODOS LOS EXAMENES POR LISTA A PARTIR DEL INDICE INGRESADO.")
        '    Console.WriteLine(" [3] SOLO UN EXAMEN SEGUN NUMERO DE ORDEN INGRESADO.")
        '    Console.WriteLine()
        '    Console.Write("INGRESA UNA OPCION: ")
        '    optn = Console.ReadLine()
        'Loop Until (isValidOption(optn))

        'Dim ind As Integer = 0

        'Select Case optn
        '    Case "1"
        '        ' Nothing
        '    Case "2"
        '        Console.WriteLine()
        '        Console.Write("INGRESAR RESPUESTAS A PARTIR DEL INDICE: ")
        '        Integer.TryParse(Console.ReadLine(), ind)
        '    Case "3"
        '        Console.WriteLine()
        '        Console.Write("INGRESAR RESPUESTAS DE ALUMNO CON NUMERO DE ORDEN: ")
        '        Integer.TryParse(Console.ReadLine(), ind)
        'End Select

        'Console.WriteLine()

        'For Each std As Student In stdlst

        '    Select Case optn
        '        Case "1"
        '            ' Nothing
        '        Case "2"
        '            If stdlst.IndexOf(std) < ind - 1 Then
        '                Continue For
        '            End If
        '        Case "3"
        '            If stdlst.IndexOf(std) <> ind - 1 Then
        '                Continue For
        '            End If
        '    End Select

        '    Console.WriteLine(std.Name)
        '    Console.WriteLine()

        '    Dim grade As Double = 0
        '    Dim score As Double = 0

        '    Dim t1 As DateTime
        '    Dim t2 As DateTime

        '    Dim ans
        '    Dim ansStr

        '    Dim isValidInput = Function(x)
        '                           If Not Regex.IsMatch(x, "^[01]{4,4}$") Then
        '                               Console.WriteLine(" PATRON DE RESPUESTA NO VALIDO, INTENTALO DE NUEVO.")
        '                               Console.WriteLine()
        '                               Return False
        '                           End If
        '                           Return True
        '                       End Function

        '    Do
        '        grade = 0
        '        t1 = Date.Now()
        '        For Each preg In pdata
        '            Do
        '                Console.Write(" " & preg.Name & ControlChars.Tab & "  :   ")
        '                ansStr = Console.ReadLine()
        '            Loop While (Not isValidInput(ansStr))

        '            ans = Convert.ToInt32(ansStr, 2)
        '            grade += IIf(ans = preg.Answer, preg.Score, 0)
        '            score = IIf(ans = preg.Answer, preg.Score, 0)
        '            Console.WriteLine(" SCORE" & ControlChars.Tab & "  :   " & score)
        '            Console.WriteLine()
        '            Console.Write(ControlChars.Cr)
        '        Next
        '        t2 = Date.Now()
        '        Console.WriteLine()
        '        Console.WriteLine(std.Name)
        '        Console.WriteLine("NOTA EXAMEN TEORICO: " & grade)
        '        Console.Write("CONFIRMAR EVALUACION (SI/NO): ")
        '    Loop While (Console.ReadLine().Trim().ToUpper() = "NO")

        '    tlst.Add(t2.Subtract(t1).Seconds)
        '    std.Grades.Item(Evaluation.EF) = grade
        '    Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
        '        serializer.Serialize(writer, stdlst)
        '    End Using
        '    Console.WriteLine()
        'Next

        ''Dim tscant = tlst.Count
        ''Dim tsmin = New TimeSpan(tlst.Min * )
        ''Dim tsmax = New TimeSpan(0, 0, tlst.Max)
        ''Dim tsprom = New TimeSpan(0, 0, tlst.Average)
        ''Dim tstotal = New TimeSpan(0, 0, tlst.Sum)

        ''Console.WriteLine()
        ''Console.WriteLine()
        ''Console.WriteLine("EXAMENES CORREGIDOS      :   " & tscant)
        ''Console.WriteLine("TIEMPO EXAMEN MIN.       :   " & tsmin.Minutes & "m " & tsmin.Seconds & "s")
        ''Console.WriteLine("TIEMPO EXAMEN MAX.       :   " & tsmax.Minutes & "m " & tsmax.Seconds & "s")
        ''Console.WriteLine("TIEMPO EXAMEN PROM.      :   " & tsprom.Minutes & "m " & tsprom.Seconds & "s")
        ''Console.WriteLine("TIEMPO TOTAL             :   " & tstotal.Minutes & "m " & tstotal.Seconds & "s")

        'Console.WriteLine()
        'Console.WriteLine("¡SILVIA ERES LO MAXIMO! :3")


        'Dim lst = StudentIO.GetStudentsFromJson("sim162-lst-json.txt")
        'Dim sb As New StringBuilder()
        'Dim id, name, p2

        'Using sw As StreamWriter = New StreamWriter("sim162-p2-xls.txt")
        '    For Each std As Student In lst
        '        id = std.Id
        '        name = std.Name
        '        p2 = Math.Round(std.Grades(Evaluation.P2), 2)
        '        sb.AppendLine(String.Join(ControlChars.Tab, New String() {id, name, p2}))
        '    Next
        '    sw.Write(sb.ToString())
        'End Using

        Return 0
    End Function

End Module
