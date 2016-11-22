Imports System.IO
Imports System.Text
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

        Dim lst = StudentIO.GetStudentsFromJson("so162-lst-json.txt")
        Dim sb As New StringBuilder()
        Dim id, name, p1

        Using sw As StreamWriter = New StreamWriter("so162-p1-xls.txt")
            For Each std As Student In lst
                id = std.Id
                name = std.Name
                p1 = std.Grades(Evaluation.P1)
                sb.AppendLine(String.Join(ControlChars.Tab, New String() {id, name, p1}))
            Next
            sw.Write(sb.ToString())
        End Using

        Exit Function

        'Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "so162-p1-txt.txt")

        'Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(path)

        '    Dim std As Student
        '    Dim p1, p2, p3, p4
        '    Dim p51, p61, p71, p81
        '    Dim p52, p62, p72, p82
        '    Dim score

        '    Dim currentRow As String()
        '    Dim sb As New StringBuilder()

        '    Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}

        '    MyReader.TextFieldType = FileIO.FieldType.Delimited
        '    MyReader.SetDelimiters(vbTab)

        '    While Not MyReader.EndOfData
        '        Try
        '            currentRow = MyReader.ReadFields()
        '            std = lst.Find(Function(x) x.Name.StartsWith(currentRow.ElementAt(0).Split(" ")(0), StringComparison.OrdinalIgnoreCase))

        '            p1 = currentRow.ElementAt(1) : p2 = currentRow.ElementAt(2) : p3 = currentRow.ElementAt(3) : p4 = currentRow.ElementAt(4)
        '            p51 = currentRow.ElementAt(5) : p61 = currentRow.ElementAt(7) : p71 = currentRow.ElementAt(9) : p81 = currentRow.ElementAt(11)
        '            p52 = currentRow.ElementAt(6) : p62 = currentRow.ElementAt(8) : p72 = currentRow.ElementAt(10) : p82 = currentRow.ElementAt(12)

        '            score = 0

        '            ' PREGUNTA 1
        '            score += IIf(p1.Contains("Simple"), 1, 0)
        '            score += IIf(p1.Contains("Limpio"), 1, 0)

        '            ' PREGUNTA 2 
        '            score += IIf(p2.Contains("Programa"), 1, 0)
        '            score += IIf(String.IsNullOrEmpty(p2), 2, 0)

        '            ' PREGUNTA 3 
        '            score += IIf(p3.Contains("nanosegundo"), 0.5, 0)
        '            score += IIf(p3.Contains("sector"), 0.5, 0)
        '            score += IIf(p3.Contains("dispositivo"), 0.5, 0)
        '            score += IIf(p3.Contains("archivo"), 0.5, 0)

        '            ' PREGUNTA 4 
        '            score += IIf(p4.Contains("monitorear sus recursos continuamente"), 0.5, 0)
        '            score += IIf(p4.Contains("determinar quien obtiene que, cuando y cuanto"), 0.5, 0)
        '            score += IIf(p4.Contains("asignar recursos cuando es apropiado"), 0.5, 0)
        '            score += IIf(p4.Contains("desasignar recursos cuando es apropiado"), 0.5, 0)

        '            lst.Find(Function(x) x.Name.StartsWith(currentRow.ElementAt(0).Split(" ")(0), StringComparison.OrdinalIgnoreCase)).Grades.Item(Evaluation.P1) = score

        '            sb.Clear()
        '            sb.AppendLine()
        '            sb.AppendLine(New String("-", std.Name.Length + 4))
        '            sb.AppendLine("- " & std.Name & " -")
        '            sb.AppendLine(New String("-", std.Name.Length + 4))
        '            sb.AppendLine()

        '            sb.AppendLine("PREGUNTA 1")
        '            sb.AppendLine("----------")
        '            sb.AppendLine(" " & p1.Replace(";", Environment.NewLine & " "))
        '            sb.AppendLine()

        '            sb.AppendLine("PREGUNTA 2")
        '            sb.AppendLine("----------")
        '            sb.AppendLine(" " & p2.Replace(";", Environment.NewLine & " "))
        '            sb.AppendLine()

        '            sb.AppendLine("PREGUNTA 3")
        '            sb.AppendLine("----------")
        '            sb.AppendLine(" " & p3.Replace(";", Environment.NewLine & " "))
        '            sb.AppendLine()

        '            sb.AppendLine("PREGUNTA 4")
        '            sb.AppendLine("----------")
        '            sb.AppendLine(" " & p4.Replace(";", Environment.NewLine & " "))
        '            sb.AppendLine()

        '            sb.AppendLine("NOTA PARCIAL")
        '            sb.AppendLine("------------")
        '            sb.AppendLine(CDbl(score).ToString("#0.00"))

        '            Console.WriteLine(sb.ToString())
        '            Console.Write("Continuar...")
        '            Console.ReadKey()

        '            ' Gestion de Procesos
        '            sb.Clear()
        '            sb.AppendLine()
        '            sb.AppendLine()
        '            sb.AppendLine("-------------------")
        '            sb.AppendLine("GESTION DE PROCESOS")
        '            sb.AppendLine("-------------------")

        '            sb.AppendLine("ADMINISTRA:")
        '            sb.AppendLine(p51)
        '            sb.AppendLine()

        '            sb.AppendLine("RESPONSABLE:")
        '            sb.AppendLine(p52)
        '            sb.AppendLine()

        '            sb.AppendLine("NOTA PARCIAL: " & CDbl(score).ToString("#0.00"))
        '            Console.Write(sb.ToString())
        '            Console.Write("PUNTAJE: ")
        '            score += CDbl(Console.ReadLine())

        '            ' Gestion de Memoria
        '            sb.Clear()
        '            sb.AppendLine()
        '            sb.AppendLine("-------------------")
        '            sb.AppendLine("GESTION DE MEMORIA")
        '            sb.AppendLine("-------------------")
        '            sb.AppendLine()

        '            sb.AppendLine("ADMINISTRA:")
        '            sb.AppendLine(p61)
        '            sb.AppendLine()

        '            sb.AppendLine("RESPONSABLE:")
        '            sb.AppendLine(p62)
        '            sb.AppendLine()

        '            sb.AppendLine("NOTA PARCIAL: " & CDbl(score).ToString("#0.00"))
        '            Console.Write(sb.ToString())
        '            Console.Write("PUNTAJE: ")
        '            score += CDbl(Console.ReadLine())

        '            ' Gestion de Archivos
        '            sb.Clear()
        '            sb.AppendLine()
        '            sb.AppendLine("-------------------")
        '            sb.AppendLine("GESTION DE ARCHIVOS")
        '            sb.AppendLine("-------------------")
        '            sb.AppendLine()

        '            sb.AppendLine("ADMINISTRA:")
        '            sb.AppendLine(p71)
        '            sb.AppendLine()

        '            sb.AppendLine("RESPONSABLE:")
        '            sb.AppendLine(p72)
        '            sb.AppendLine()

        '            sb.AppendLine("NOTA PARCIAL: " & CDbl(score).ToString("#0.00"))
        '            Console.Write(sb.ToString())
        '            Console.Write("PUNTAJE: ")
        '            score += CDbl(Console.ReadLine())

        '            ' Gestion de Dispositivos
        '            sb.Clear()
        '            sb.AppendLine()
        '            sb.AppendLine("-----------------------")
        '            sb.AppendLine("GESTION DE DISPOSITIVOS")
        '            sb.AppendLine("-----------------------")
        '            sb.AppendLine()

        '            sb.AppendLine("ADMINISTRA:")
        '            sb.AppendLine(p81)
        '            sb.AppendLine()

        '            sb.AppendLine("RESPONSABLE:")
        '            sb.AppendLine(p82)
        '            sb.AppendLine()

        '            sb.AppendLine("NOTA PARCIAL: " & CDbl(score).ToString("#0.00"))
        '            Console.Write(sb.ToString())
        '            Console.Write("PUNTAJE: ")
        '            score += CDbl(Console.ReadLine())

        '            Console.WriteLine()
        '            Console.WriteLine(std.Name & ": " & CDbl(score).ToString("#0.00"))
        '            'Console.WriteLine(std.Name)
        '            'Console.WriteLine("NOTA: " & CDbl(score).ToString("#0.00"))
        '            Console.Write("Continuar...")
        '            Console.ReadKey()

        '            std.Grades(Evaluation.P1) = score

        '            Using sw As New StreamWriter(IO.Path.Combine(My.Application.Info.DirectoryPath, "so162-lst-json.txt")), writer As New JsonTextWriter(sw)
        '                serializer.Serialize(writer, lst)
        '            End Using

        '            'sb.AppendLine("PREGUNTA 1: (Simple;Limpio)")
        '            'sb.AppendLine(p1 & vbCrLf)
        '            'sb.AppendLine("PREGUNTA 2 " & p2 & " (Programa - Hardware - Ordenador)")
        '            'sb.AppendLine("PREGUNTA 3 " & p3 & " (nanosegundo;sector;dispositivo;archivo)")
        '            'sb.AppendLine("PREGUNTA 4 " & p4 & " (monitorear sus recursos continuamente;determinar quien obtiene que, cuando y cuanto;asignar recursos cuando es apropiado;desasignar recursos cuando es apropiado)")

        '        Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
        '            Console.WriteLine("Line " & ex.Message & "is not valid and will be skipped.")
        '        End Try
        '    End While

        '    Console.WriteLine()

        'End Using

        'Dim lst = StudentIO.GetStudentsFromJson("so162-lst-json.txt")
        'Dim c As Char = lst.Find(Function(x) x.Name.StartsWith("LI")).Name.ToCharArray()(2)
        'For Each std As Student In lst
        '    If std.Name.Contains(c) Then std.Name = std.Name.Replace(c, "Ñ")
        'Next

        'Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        'Using sw As New StreamWriter(IO.Path.Combine(My.Application.Info.DirectoryPath, "so162-lst-json.txt")), writer As New JsonTextWriter(sw)
        '    serializer.Serialize(writer, lst)
        'End Using


        'Dim lst = StudentIO.GetStudentsFromJson("so162-lst-json.txt")
        'Dim newStd = New Student()
        'With newStd
        '    .Id = "0000000000"
        '    .Name = "ORELLANA POVIS, JOSETH"
        '    .Grades.Add(Evaluation.P1, 0)
        '    .Grades.Add(Evaluation.EP, 0)
        '    .Grades.Add(Evaluation.P2, 0)
        '    .Grades.Add(Evaluation.EF, 0)
        'End With

        'lst.Add(newStd)
        'lst.Sort(Function(x, y) x.Name.CompareTo(y.Name))

        'Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "so162-lst-json.txt")
        'Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        'Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
        '    serializer.Serialize(writer, lst)
        'End Using



        'Dim sb As New StringBuilder()
        'Dim stdlst = StudentIO.GetStudentsFromTextFile("so162-lst.txt")
        'Dim json = JsonConvert.SerializeObject(stdlst, Formatting.Indented)

        'For Each std As Student In stdlst
        '    sb.AppendLine(String.Join(ControlChars.Tab, New String() {std.Id, std.Name}))
        'Next

        'Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        'Dim fileName = "so162-lst-json.txt"
        'Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, fileName)

        'Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
        '    serializer.Serialize(writer, stdlst)
        'End Using

        'Console.WriteLine(sb.ToString())

        'Console.WriteLine()
        'Console.WriteLine("¡Hello World!")

        'Dim sb As New StringBuilder()
        'Dim stdlst = StudentIO.GetStudentsJson("sim162-lst-json.txt")
        'Dim id, name, p1, ep, p2, ef

        'Using sw As StreamWriter = New StreamWriter("sim162-ep-xls.txt")
        '    For Each std As Student In stdlst
        '        id = std.Id
        '        name = std.Name
        '        'p1 = std.Grades(Evaluation.P1)
        '        ep = std.Grades(Evaluation.EP)
        '        'p2 = std.Grades(Evaluation.P2)
        '        'ef = std.Grades(Evaluation.EF)
        '        'sb.AppendLine(String.Join(ControlChars.Tab, New String() {id, name, p1}))
        '        sb.AppendLine(String.Join(ControlChars.Tab, New String() {id, name, ep}))
        '        'sb.AppendLine(String.Join(ControlChars.Tab, New String() {id, name, p1, ep, p2, ef}))
        '    Next
        '    sw.Write(sb.ToString())
        'End Using

        'Console.WriteLine(sb.ToString())

        'Console.WriteLine("-------------------------------")
        'Console.WriteLine("- INGRESO NOTA EXAMEN PARCIAL -")
        'Console.WriteLine("-    SMART EVALUATION MODE    -")
        'Console.WriteLine("-------------------------------")
        'Console.WriteLine()


        'Console.WriteLine("Ingrese sus respuestas... ")
        'Console.WriteLine()



        'Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        'Dim fileName = "sim162-lst-json.txt"
        'Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, fileName)

        'Dim grde As Double
        'Dim stdlst = StudentIO.GetStudentsJson("sim162-lst-json.txt")

        'Dim p1str = "1. ¿Cuál fue el número de espectadores que ingresaron al cine durante la simulación?"
        'Dim p21str = "2.1. ¿En cuánto se distribuye la asistencia de espectadores regulares?"
        'Dim p22str = "2.2. ¿En cuánto se distribuye la asistencia de espectadores afiliados?"
        'Dim p3str = "3. ¿Cuál fue el tiempo promedio que toma un espectador regular antes de ingresar a la sala de cine?"
        'Dim p4str = "4. ¿Cuál fue el mayor tiempo que un espectador afiliado estuvo en espera?"
        'Dim p5str = "5. ¿Cuál fue el número de espectadores que eligieron comprar productos en confitería?"
        'Dim p6str = "6. ¿Qué proporción del tiempo estuvo ocupado el Cajero de Confitería?"

        'Dim p1 = New With {Key .Name = p1str, .Answer = 234, .Score = 2}
        'Dim p21 = New With {Key .Name = p21str, .Answer = 44, .Score = 1}
        'Dim p22 = New With {Key .Name = p22str, .Answer = 190, .Score = 1}
        'Dim p3 = New With {Key .Name = p3str, .Answer = 17.69, .Score = 2}
        'Dim p4 = New With {Key .Name = p4str, .Answer = 38.68, .Score = 2}
        'Dim p5 = New With {Key .Name = p5str, .Answer = 180, .Score = 2}
        'Dim p6 = New With {Key .Name = p6str, .Answer = 60.65, .Score = 2}

        'Dim pdata = New ArrayList({p1, p21, p22, p3, p4, p5, p6})

        'For Each std As Student In stdlst

        '    Console.WriteLine("-")
        '    Console.WriteLine(std.Name)


        '    grde = std.Grades(Evaluation.P1)

        '    If grde > 0 Then
        '        Console.WriteLine("NOTA P1: " & grde)
        '        Continue For
        '    End If

        '    Dim grdeT, grdeL

        '    Console.Write("NOTA EX. TEORICO: ")
        '    grdeT = Double.Parse(Console.ReadLine())

        '    Console.WriteLine("SMART EVAL > EX. LABORATORIO: ")

        '    Do
        '        grdeL = 0
        '        For Each preg In pdata
        '            Console.WriteLine()
        '            Console.WriteLine(preg.Name)
        '            Console.Write("RPTA: ")
        '            Dim ans = Double.Parse(Console.ReadLine())
        '            If Math.Round(ans, 2) = preg.Answer Then
        '                grdeL += preg.Score
        '                Console.WriteLine("CORRECTO (" & preg.Answer & ")")
        '            Else
        '                Console.WriteLine("INCORRECTO (" & preg.Answer & ")")
        '            End If
        '            Console.Write(ControlChars.Cr)
        '        Next
        '        grde = grdeL + grdeT
        '        Console.WriteLine("NOTA: " & grde)
        '        Console.Write("CONFIRMAR EVALUACION (SI/NO): ")
        '    Loop While (Console.ReadLine().Trim().ToUpper() = "NO")

        '    std.Grades.Item(Evaluation.P1) = grde

        '    Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
        '        serializer.Serialize(writer, stdlst)
        '    End Using
        'Next

        'Dim json = JsonConvert.SerializeObject(stdlst, Formatting.Indented)
        'Console.WriteLine(json)

        'For Each std As Student In stdlst
        '    If std.Grades(Evaluation.P1) > 0 Then Continue For
        '    Console.WriteLine("-")
        '    Console.WriteLine(std.Name)
        '    Console.ReadKey()
        'Next


        'Console.Write("INGRESE CODIGO: ")
        'Dim id = Console.ReadLine()
        'If Not stdlst.Exists(Function(x) x.Id.Equals(id)) Then
        '    Console.WriteLine("Not Found!")
        '    Exit Function
        'End If

        'Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        'Dim fileName = "sim162-lst-json.txt"
        'Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, fileName)

        'Dim grde As Double
        'Dim eval As Evaluation
        'Dim stdlst = StudentIO.GetStudentsJson("sim162-lst-json.txt")

        'For Each std As Student In stdlst

        '    Console.WriteLine("-")
        '    Console.WriteLine(std.Name)
        '    Console.Write("NOTA P1: ")

        '    eval = Evaluation.P1
        '    grde = Double.Parse(Console.ReadLine())
        '    std.Grades.Item(eval) = grde

        '    Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
        '        serializer.Serialize(writer, stdlst)
        '    End Using
        'Next

        'Dim json = JsonConvert.SerializeObject(stdlst, Formatting.Indented)
        'Console.WriteLine(json)

        'Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        'Dim fileName = "sim162-lst-json.txt"
        'Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, fileName)

        'Dim grde As Double
        'Dim eval As Evaluation

        'For Each std As Student In stdlst
        '    Console.WriteLine(std.Id & " " & std.Name)
        '    eval = Evaluation.EP
        '    grde = StudentIO.CalcGrade(Course.sim162, Evaluation.EP)
        '    std.Grades.Item(eval) = grde
        '    Console.WriteLine("NOTA: " & grde & vbCrLf)
        '    Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
        '        serializer.Serialize(writer, stdlst)
        '    End Using
        'Next

        'Dim json = JsonConvert.SerializeObject(stdlst, Formatting.Indented)
        'Console.WriteLine(json)

        'For Each std As Student In stdlst
        '    Console.WriteLine(std.Id & " " & std.Name)
        '    eval = Evaluation.EP
        '    grde = StudentIO.CalcGrade(Course.sim162, Evaluation.EP)
        '    std.Grades.Item(eval) = grde
        '    Console.WriteLine("NOTA: " & grde & vbCrLf)
        '    Console.WriteLine()
        'Next

        'Dim sb = New StringBuilder()
        'Dim stdLst = StudentIO.GetStudents("sim162-lst.txt")

        'sb.AppendLine()
        'sb.AppendLine(" " & HeadingInfo.Default.ToString)
        'sb.AppendLine()

        'sb.AppendFormat("  {0,-10}  {1, -40} {2}", "Id", "Name", vbCrLf)

        'For Each std As Student In stdLst
        '    sb.AppendFormat("  {0,-10}  {1, -40} {2}", std.Id, std.Name, vbCrLf)
        'Next

        'Console.WriteLine()
        'Console.WriteLine(sb.ToString())


        'Dim sb = New StringBuilder()
        'Dim stdLst = StudentIO.GetStudents("sim162-lst.txt")

        'sb.AppendLine()
        'sb.AppendLine("  " & HeadingInfo.Default.ToString)
        'sb.AppendLine()

        'sb.AppendFormat("  {0,-10}  {1, -40} P1  EP  P2  EF  PF {2}", "Id", "Name", vbCrLf)

        'For Each std As Student In stdLst
        '    sb.AppendFormat("  {0,-10} {1, -40} {2:00} {}", std.Id, std.Name,
        '                                                    std.Grades(Evaluation.P1),
        '                                                    std.Grades(Evaluation.EP),
        '                                                    std.Grades(Evaluation.P2),
        '                                                    std.Grades(Evaluation.EF),
        '                                                    std.FinalGrade,
        '                                                    vbCrLf)
        'Next

        'Console.WriteLine()
        'Console.WriteLine(sb.ToString())

        'Dim stdLst = StudentIO.GetStudents("visual162-lst.txt")
        'StudentIO.SetGrades("sim162-lst.txt", "prac01.txt")

        'Dim stdLst = StudentIO.GetGrades("sim162-lst.txt")
        'Dim json = JsonConvert.SerializeObject(stdLst, Formatting.Indented)

        'Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        'Dim fileName = "sim162-lst-json.txt"
        'Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, fileName)

        'Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
        '    serializer.Serialize(writer, stdLst)
        'End Using

        'Dim stdLstJson As List(Of Student)
        'Using file As New StreamReader(path)
        '    stdLstJson = serializer.Deserialize(file, GetType(List(Of Student)))
        '    If stdLstJson Is Nothing Then
        '        Console.WriteLine("Nothing")
        '    Else
        '        Console.WriteLine("Succeed")
        '        Console.WriteLine(stdLstJson.Count)
        '    End If
        'End Using

        '// read file into a string And deserialize JSON to a type
        'Movie movie1 = JsonConvert.DeserializeObject<Movie>(File.ReadAllText(@"c:\movie.json"));
        '
        '// deserialize JSON directly from a file
        'USing (StreamReader file = File.OpenText(@"c:\movie.json"))
        '{
        '   JsonSerializer serializer = New JsonSerializer();
        '   Movie movie2 = (Movie)serializer.Deserialize(file, TypeOf(Movie));
        '}

        'Console.WriteLine(json)
        Return 0

    End Function

End Module
