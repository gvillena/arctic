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
                                              Sim171Options,
                                              Se171Options,
                                              So162Options,
                                              Visual162Options,
                                              TestOptions)(args)

        ' Execute parsed action
        result.WithParsed(Of Pweb162Options)(AddressOf Pweb162).
                WithParsed(Of Sim162Options)(AddressOf Sim162).
                WithParsed(Of Sim171Options)(AddressOf Sim171).
                WithParsed(Of Se171Options)(AddressOf Se171).
                WithParsed(Of So162Options)(AddressOf So162).
                WithParsed(Of Visual162Options)(AddressOf Visual162).
                WithParsed(Of TestOptions)(AddressOf Test).
                WithNotParsed(Function(errors) 1)

    End Sub

    Function Pweb162(opts As Pweb162Options)

        Console.WriteLine("--------------------------")
        Console.WriteLine("| ARCTIC                 |")
        Console.WriteLine("| PLATAFORMA WEB 2016-2  |")
        Console.WriteLine("--------------------------")

        Dim optn As String
        Dim isValidOption = Function(x)
                                If Not Regex.IsMatch(x, "^[12]{1,1}$") Then
                                    Console.WriteLine("OPCION NO VALIDA, INTENTALO DE NUEVO.")
                                    Console.WriteLine()
                                    Return False
                                End If
                                Return True
                            End Function

        Do

            Console.WriteLine()
            Console.WriteLine("MENU PRINCIPAL")
            Console.WriteLine()
            Console.WriteLine("  [1] EVALUATION GRADE MANAGER")
            Console.WriteLine("  [2] EVALUATION FILES MANAGER")
            Console.WriteLine()
            Console.Write("INGRESA UNA OPCION: ")
            optn = Console.ReadLine()
        Loop Until (isValidOption(optn))

        Select Case optn
            Case "1"
                Pweb162M.InitGradeMngr()
            Case "2"
                Pweb162M.InitEvalFilesMngr()
        End Select

        Return 0

    End Function

    Function Sim162(opts As Sim162Options)

        Console.WriteLine()
        Console.WriteLine(" -------------------------------")
        Console.WriteLine("| ARCTIC                        |")
        Console.WriteLine("| SIMULACION DE SISTEMAS 2016-2 |")
        Console.WriteLine(" -------------------------------")

        Dim optn As String
        Dim isValidOption = Function(x)
                                If Not Regex.IsMatch(x, "^[12]{1,1}$") Then
                                    Console.WriteLine("OPCION NO VALIDA, INTENTALO DE NUEVO.")
                                    Console.WriteLine()
                                    Return False
                                End If
                                Return True
                            End Function

        Do
            Console.WriteLine()
            Console.WriteLine("MENU PRINCIPAL")
            Console.WriteLine()
            Console.WriteLine("  [1] EVALUATION GRADE MANAGER")
            Console.WriteLine("  [2] EVALUATION FILES MANAGER")
            Console.WriteLine()
            Console.Write("INGRESA UNA OPCION: ")
            optn = Console.ReadLine()
        Loop Until (isValidOption(optn))

        Select Case optn
            Case "1"
                Sim162M.InitGradeMngr()
            Case "2"
                Sim162M.InitEvalFilesMngr()
        End Select

        'Dim opc As String = String.Empty

        'Console.WriteLine("-----------------------------")
        'Console.WriteLine("Simulacion de Sistemas 2016-2")
        'Console.WriteLine("-----------------------------")
        'Console.WriteLine()
        'Console.WriteLine("[1] Lista")
        'Console.WriteLine("[2] Notas P1")
        'Console.WriteLine("[3] Notas EP")
        'Console.WriteLine("[4] Notas P2")
        'Console.WriteLine("[5] Notas EF")
        'Console.WriteLine("[6] Notas P1 / EP / P2 / EF")
        'Console.WriteLine("[7] Promedios Finales")
        'Console.WriteLine("[8] Promedios Finales (Notas)")
        'Console.WriteLine("[9] Promedios Finales (Notas / Susti)")
        'Console.WriteLine("[10] Salir")

        'Console.WriteLine()
        'Console.Write("Seleccione una opcion: ")
        'opc = Console.ReadLine()

        'Select Case opc

        '    ' Mostrar Lista
        '    Case "1"

        '        Dim sb = New StringBuilder()
        '        Dim stdlst = StudentIO.GetStudentsJson("sim162-lst-json.txt")

        '        sb.AppendFormat(" {0,-10}  {1, -40} {2}", "CODIGO", "APELLIDOS Y NOMBRES", vbCrLf)
        '        For Each std As Student In stdlst
        '            sb.AppendFormat(" {0,-10}  {1, -40} {2}", std.Id, std.Name, vbCrLf)
        '        Next
        '        Console.WriteLine()
        '        Console.WriteLine(sb.ToString())

        '    Case "2"

        '        Console.WriteLine("NOTAS PRACTICA 01")
        '        Console.WriteLine()
        '        Console.WriteLine("[1] Mostrar")
        '        Console.WriteLine("[2] Ingresar")
        '        Console.WriteLine("[3] Modificar")
        '        Console.WriteLine("[4] Salir")
        '        Console.WriteLine()
        '        Console.Write("Seleccione una opcion: ")
        '        opc = Console.ReadLine()

        '        Select Case opc
        '            Case 1
        '                Dim sb = New StringBuilder()
        '                Dim stdlst = StudentIO.GetStudentsJson("sim162-lst-json.txt")
        '                sb.AppendFormat(" {0,-10}  {1, -40} {2:00.00} {3}", "CODIGO", "APELLIDOS Y NOMBRES", "P1", vbCrLf)

        '                For Each std As Student In stdlst
        '                    sb.AppendFormat(" {0,-10}  {1, -40} {2:00.00} {3}", std.Id, std.Name,
        '                                                                        std.Grades(Evaluation.P1),
        '                                                                        vbCrLf)
        '                Next
        '                Console.WriteLine()
        '                Console.WriteLine(sb.ToString())

        '            Case 2

        '                Console.WriteLine("INGRESAR NOTAS PRACTICA 01")
        '                Console.WriteLine("--------------------------")
        '                Console.WriteLine("MODOS DE INGRESO:")
        '                Console.WriteLine()
        '                Console.WriteLine(" [1] Normal")
        '                Console.WriteLine(" [2] SmartEval")
        '                Console.WriteLine(" [3] Salir")
        '                Console.WriteLine()
        '                Console.Write("Seleccione un modo de ingreso: ")
        '                opc = Console.ReadLine()

        '                Select Case opc
        '                    Case 1
        '                        Console.Write("Ingrese codigo de alumno o presione 'Enter' para todos los alumnos: ")
        '                        If String.IsNullOrEmpty(opc) Then

        '                        Else
        '                            ' TODO Student One By One
        '                        End If
        '                    Case 2

        '                    Case Else

        '                End Select



        '            Case Else
        '                Console.WriteLine("Opcion no valida... :(")
        '        End Select

        '        'Dim sb = New StringBuilder()
        '        'Dim stdlst = StudentIO.GetStudentsJson("sim162-lst-json.txt")
        '        'sb.AppendFormat(" {0,-10}  {1, -40} {2:00.00} {3:00.00} {4:00.00} {5:00.00} {6}", "CODIGO",
        '        '                                                                                  "APELLIDOS Y NOMBRES",
        '        '                                                                                  "P1", "EP", "P2", "EF",
        '        '                                                                                  vbCrLf)
        '        'For Each std As Student In stdlst
        '        '    sb.AppendFormat(" {0,-10}  {1, -40} {2:00.00} {3:00.00} {4:00.00} {5:00.00} {6}", std.Id,
        '        '                                                                                      std.Name,
        '        '                                                                                      std.Grades(Evaluation.P1),
        '        '                                                                                      std.Grades(Evaluation.EP),
        '        '                                                                                      std.Grades(Evaluation.P2),
        '        '                                                                                      std.Grades(Evaluation.EF),
        '        '                                                                                      vbCrLf)
        '        'Next
        '        'Console.WriteLine()
        '        'Console.WriteLine(sb.ToString())

        '    Case "3"
        '        Console.WriteLine("TODO")
        '    Case "4"
        '        Console.WriteLine("TODO")
        '    Case Else
        '        Console.WriteLine("Opcion no valida... :(")
        'End Select
        Return 0
    End Function

    Function Sim171(opts As Sim171Options)

        Console.WriteLine()
        Console.WriteLine(" ------------------------------- ")
        Console.WriteLine("| ARCTIC                        |")
        Console.WriteLine("| SIMULACION DE SISTEMAS 2017-1 |")
        Console.WriteLine(" ------------------------------- ")

        Dim optn As String
        Dim isValidOption = Function(x)
                                If Not Regex.IsMatch(x, "^[12345]{1,1}$") Then
                                    Console.WriteLine("OPCION NO VALIDA, INTENTALO DE NUEVO.")
                                    Console.WriteLine()
                                    Return False
                                End If
                                Return True
                            End Function

        Do
            Console.WriteLine()
            Console.WriteLine("MENU PRINCIPAL")
            Console.WriteLine()
            Console.WriteLine("  [1] PRIMERA PRACTICA")
            Console.WriteLine("  [2] EXAMEN PARCIAL")
            Console.WriteLine("  [3] SEGUNDA PRACTICA")
            Console.WriteLine("  [4] EXAMEN FINAL")
            Console.WriteLine("  [5] MOSTRAR NOTAS")
            Console.WriteLine()
            Console.Write("INGRESA UNA OPCION: ")
            optn = Console.ReadLine()
        Loop Until (isValidOption(optn))

        Select Case optn
            Case "1" ' PRACTICA 01
                Sim171M.InitSmartEvalP1()

            Case "2" ' EX. PARCIAL
                Sim171M.InitSmartEvalEP()

            Case "3" ' PRACTICA 02
                Sim171M.InitSmartEvalP2()

            Case "4" ' EX. FINAL
                Sim171M.InitSmartEvalEF()

            Case "5" ' MOSTRAR NOTAS
                'Sim171M.I
        End Select

        Return 0
    End Function

    Function Se171(opts As Se171Options)

        Console.WriteLine()
        Console.WriteLine(" ------------------------------- ")
        Console.WriteLine("| ARCTIC                        |")
        Console.WriteLine("| SISTEMAS EMPRESARIALES 2017-1 |")
        Console.WriteLine(" ------------------------------- ")

        Dim optn As String
        Dim isValidOption = Function(x)
                                If Not Regex.IsMatch(x, "^[12345]{1,1}$") Then
                                    Console.WriteLine("OPCION NO VALIDA, INTENTALO DE NUEVO.")
                                    Console.WriteLine()
                                    Return False
                                End If
                                Return True
                            End Function

        Do
            Console.WriteLine()
            Console.WriteLine("MENU PRINCIPAL")
            Console.WriteLine()
            Console.WriteLine("  [1] PRIMERA PRACTICA")
            Console.WriteLine("  [2] EXAMEN PARCIAL")
            Console.WriteLine("  [3] SEGUNDA PRACTICA")
            Console.WriteLine("  [4] EXAMEN FINAL")
            Console.WriteLine("  [5] MOSTRAR NOTAS")
            Console.WriteLine()
            Console.Write("INGRESA UNA OPCION: ")
            optn = Console.ReadLine()
        Loop Until (isValidOption(optn))

        Select Case optn
            Case "1" ' PRACTICA 01
                Se171M.InitSmartEvalP1()

            Case "2" ' EX. PARCIAL
                Se171M.InitSmartEvalEP()

            Case "3" ' PRACTICA 02
                Se171M.InitSmartEvalP2()

            Case "4" ' EX. FINAL
                Se171M.InitSmartEvalEF()

            Case "5" ' MOSTRAR NOTAS
                Se171M.InitGradeMngr()
        End Select

        Return 0
    End Function

    Function So162(opts As So162Options)

        Console.WriteLine()
        Console.WriteLine(" -----------------------------")
        Console.WriteLine("| ARCTIC                      |")
        Console.WriteLine("| SISTEMAS OPERATIVOS 2016-2  |")
        Console.WriteLine(" -----------------------------")

        Dim optn As String
        Dim isValidOption = Function(x)
                                If Not Regex.IsMatch(x, "^[12]{1,1}$") Then
                                    Console.WriteLine("OPCION NO VALIDA, INTENTALO DE NUEVO.")
                                    Console.WriteLine()
                                    Return False
                                End If
                                Return True
                            End Function

        Do
            Console.WriteLine()
            Console.WriteLine("MENU PRINCIPAL")
            Console.WriteLine()
            Console.WriteLine("  [1] EVALUATION GRADE MANAGER")
            Console.WriteLine("  [2] EVALUATION FILES MANAGER")
            Console.WriteLine()
            Console.Write("INGRESA UNA OPCION: ")
            optn = Console.ReadLine()
        Loop Until (isValidOption(optn))

        Select Case optn
            Case "1"
                So162M.InitGradeMngr()
            Case "2"
                So162M.InitEvalFilesMngr()
        End Select

        Return 0
    End Function

    Function Visual162(opts As Visual162Options)
        Visual162M.InitGradeMngr()
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
        Console.WriteLine("Hello World!")
        Console.WriteLine()

        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "se171-lst-json.txt")
        Dim stdlst As List(Of Student) = Se171M.GetStudents()

        For Each std As Student In stdlst
            Console.WriteLine(std.Id)
            Console.WriteLine(std.Id)
        Next





        'Sim171M.InitSmartEvalP1()

        'Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        'Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "sim162-lst-json.txt")
        'Dim stdlst As List(Of Student) = Sim162M.GetStudents()

        'For Each std As Student In stdlst

        '    Console.WriteLine(std.Name)
        '    Console.Write("EVALUAR (SI/NO): ")
        '    If Console.ReadLine.Trim.Equals("NO", StringComparison.OrdinalIgnoreCase) Then
        '        Continue For
        '    End If

        '    Dim mdcpth As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        '    Dim crspth As String = IO.Path.Combine(mdcpth, "Sim162")
        '    Dim stdpth As String = IO.Path.Combine(crspth, std.Name)

        '    Dim grdP2 As Double
        '    Dim grdP2T As Double
        '    Dim grdP2L As Double

        '    Diagnostics.Process.Start(IO.Path.Combine(stdpth, "E1-PRACT02"))

        '    Do
        '        Console.WriteLine()
        '        Console.WriteLine("SEGUNDA PRACTICA")
        '        grdP2T = std.Grades(Evaluation.P2)
        '        Console.WriteLine("NOTA EX. TEORICO: " & grdP2T)
        '        Console.Write("NOTA EX. LABORAT: ")
        '        grdP2L = Double.Parse(Console.ReadLine())
        '        grdP2 = grdP2T + grdP2L
        '        Console.WriteLine("NOTA EX. PRACT02: " & grdP2)
        '        Console.Write("CONFIRMAR EVALUACION (SI/NO): ")
        '    Loop While (Console.ReadLine().Trim().ToUpper() = "NO")

        '    Dim grdEF As Double
        '    Dim grdEFT As Double
        '    Dim grdEFL As Double

        '    Diagnostics.Process.Start(IO.Path.Combine(stdpth, "E2-EXFINAL"))

        '    Do
        '        Console.WriteLine()
        '        Console.WriteLine("EXAMEN FINAL")
        '        grdEFT = std.Grades(Evaluation.EF)
        '        Console.WriteLine("NOTA EX. TEORICO: " & grdEFT)
        '        Console.Write("NOTA EX. LABORAT: ")
        '        grdEFL = Double.Parse(Console.ReadLine())
        '        grdEF = grdEFT + grdEFL
        '        Console.WriteLine("NOTA EX. FINAL: " & grdEF)
        '        Console.Write("CONFIRMAR EVALUACION (SI/NO): ")
        '    Loop While (Console.ReadLine().Trim().ToUpper() = "NO")

        '    std.Grades(Evaluation.P2) = grdP2
        '    std.Grades(Evaluation.EF) = grdEF

        '    Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
        '        serializer.Serialize(writer, stdlst)
        '    End Using


        'Next
        'Console.WriteLine("---------------------------------")
        'Console.WriteLine("- INGRESO NOTA SEGUNDA PRACTICA -")
        'Console.WriteLine("-     SMART EVALUATION MODE     -")
        'Console.WriteLine("---------------------------------")
        'Console.WriteLine()

        'Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        'Dim fileName = "so162-lst-json.txt"
        'Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, fileName)
        'Dim stdlst = StudentIO.GetStudentsJson("so162-lst-json.txt")

        'Dim p1str = "1. Un Sistema Operativo ofrece a los programas de usuario un modelo de la computadora (0.4)"
        'Dim p2str = "2. Es un ____________ que gestiona el ____________ de un __________.(0.5)"
        'Dim p3str = "3. El sistema operativo controla cada: (0.5)"
        'Dim p4str = "4. Independientemente de su rol cada subsistema debe (0.5)"
        'Dim p5str = "5. Diseña programas para ser conectados a otros programas. (0.5)"
        'Dim p6str = "6. Diseña pensando en la simplicidad; agrega complejidad solamente donde debas. (0.5) "
        'Dim p7str = "7. Cuando debas fallar, falla ruidosamente y tan pronto como sea posible. (0.5)"
        'Dim p8str = "8. Separa la política del mecanismo; separa las interfaces de los motores. (0.5)"
        'Dim p9str = "9. Escribe partes simples conectadas por interfaces limpias. (0.5)"
        'Dim p10str = "10. Diseña para el futuro, porque estará aquí más pronto de lo que piensas. (0.5)"
        'Dim p11str = "11. La __________ es mejor que la habilidad y el ingenio. (0.5)"
        'Dim p12str = "12. Un programa es una entidad ___ mientras que un proceso es una entidad _________. (1.5)"
        'Dim p13str = "13. Un ___________ es una instancia _________ de un __________ en ejecución. (1.5)"
        'Dim p14str = "14. Durante el tiempo que un proceso pase en el sistema este deberá pasar por diferentes estados (1.5)"
        'Dim p15str = "15. Cuando el proceso es aceptado por el sistema, este pasa al estado _____ y es colocado en una cola. (1.5)"
        'Dim p16str = "16. _______ indica que el proceso no puede continuar hasta que un recurso especifico sea asignado o una operación _____ deba terminar. (1.5) "
        'Dim p17str = "17. _________ indica, por supuesto, que el proceso está siendo ejecutado. (1.5)"
        'Dim p18str = "18. Desde ______, el proceso pasa al estado _______, cuando este se encuentre listo para ser ejecutado, pero está a la espera del CPU. (1.5)"
        'Dim p19str = "19. Antes que el sistema operativo pueda planificar su ejecución, este debe resolver tres limitaciones del sistema. (1.5)"
        'Dim p20str = "20. Ejecutar tantos trabajos como sea posible en una cantidad dada de tiempo. (1.5)"
        'Dim p21str = "21. Esto podría hacerse ejecutando primero todos los trabajos por lotes. (1.5)"
        'Dim p22str = "22. Esto puede hacerse ejecutando sólo trabajos con CPU (y no con trabajos de E/S). (1.5)"

        'Dim p1 = New With {Key .Name = p1str, .Answer = Alternativa.A Or Alternativa.B, .Score = 0.5}
        'Dim p2 = New With {Key .Name = p2str, .Answer = Alternativa.B, .Score = 0.5}
        'Dim p3 = New With {Key .Name = p3str, .Answer = Alternativa.A Or Alternativa.B Or Alternativa.C Or Alternativa.D, .Score = 0.5}
        'Dim p4 = New With {Key .Name = p4str, .Answer = Alternativa.A Or Alternativa.B Or Alternativa.C Or Alternativa.D, .Score = 0.5}
        'Dim p5 = New With {Key .Name = p5str, .Answer = Alternativa.B, .Score = 0.5}
        'Dim p6 = New With {Key .Name = p6str, .Answer = Alternativa.C, .Score = 0.5}
        'Dim p7 = New With {Key .Name = p7str, .Answer = Alternativa.B, .Score = 0.5}
        'Dim p8 = New With {Key .Name = p8str, .Answer = Alternativa.NoAnswer, .Score = 0.5}
        'Dim p9 = New With {Key .Name = p9str, .Answer = Alternativa.B, .Score = 0.5}
        'Dim p10 = New With {Key .Name = p10str, .Answer = Alternativa.D, .Score = 0.5}
        'Dim p11 = New With {Key .Name = p11str, .Answer = Alternativa.B, .Score = 0.5}
        'Dim p12 = New With {Key .Name = p12str, .Answer = Alternativa.NoAnswer, .Score = 1.5}
        'Dim p13 = New With {Key .Name = p13str, .Answer = Alternativa.C, .Score = 1.5}
        'Dim p14 = New With {Key .Name = p14str, .Answer = Alternativa.A Or Alternativa.B Or Alternativa.C, .Score = 1.5}
        'Dim p15 = New With {Key .Name = p15str, .Answer = Alternativa.B, .Score = 1.5}
        'Dim p16 = New With {Key .Name = p16str, .Answer = Alternativa.A, .Score = 1.5}
        'Dim p17 = New With {Key .Name = p17str, .Answer = Alternativa.NoAnswer, .Score = 1.5}
        'Dim p18 = New With {Key .Name = p18str, .Answer = Alternativa.D, .Score = 1.5}
        'Dim p19 = New With {Key .Name = p19str, .Answer = Alternativa.A Or Alternativa.B, .Score = 1.5}
        'Dim p20 = New With {Key .Name = p20str, .Answer = Alternativa.B, .Score = 1.5}
        'Dim p21 = New With {Key .Name = p21str, .Answer = Alternativa.C, .Score = 1.5}
        'Dim p22 = New With {Key .Name = p22str, .Answer = Alternativa.NoAnswer, .Score = 1.5}

        'Dim p1 = New With {Key .Name = "P1", .Answer = Alternativa.A Or Alternativa.B, .Score = 0.5}
        'Dim p2 = New With {Key .Name = "P2", .Answer = Alternativa.B, .Score = 0.5}
        'Dim p3 = New With {Key .Name = "P3", .Answer = Alternativa.A Or Alternativa.B Or Alternativa.C Or Alternativa.D, .Score = 0.5}
        'Dim p4 = New With {Key .Name = "P4", .Answer = Alternativa.A Or Alternativa.B Or Alternativa.C Or Alternativa.D, .Score = 0.5}
        'Dim p5 = New With {Key .Name = "P5", .Answer = Alternativa.B, .Score = 0.5}
        'Dim p6 = New With {Key .Name = "P6", .Answer = Alternativa.C, .Score = 0.5}
        'Dim p7 = New With {Key .Name = "P7", .Answer = Alternativa.B, .Score = 0.5}
        'Dim p8 = New With {Key .Name = "P8", .Answer = Alternativa.NoAnswer, .Score = 0.5}
        'Dim p9 = New With {Key .Name = "P9", .Answer = Alternativa.B, .Score = 0.5}
        'Dim p10 = New With {Key .Name = "P10", .Answer = Alternativa.D, .Score = 0.5}
        'Dim p11 = New With {Key .Name = "P11", .Answer = Alternativa.B, .Score = 0.5}
        'Dim p12 = New With {Key .Name = "P12", .Answer = Alternativa.NoAnswer, .Score = 1.5}
        'Dim p13 = New With {Key .Name = "P13", .Answer = Alternativa.C, .Score = 1.5}
        'Dim p14 = New With {Key .Name = "P14", .Answer = Alternativa.A Or Alternativa.B Or Alternativa.C, .Score = 1.5}
        'Dim p15 = New With {Key .Name = "P15", .Answer = Alternativa.B, .Score = 1.5}
        'Dim p16 = New With {Key .Name = "P16", .Answer = Alternativa.A, .Score = 1.5}
        'Dim p17 = New With {Key .Name = "P17", .Answer = Alternativa.NoAnswer, .Score = 1.5}
        'Dim p18 = New With {Key .Name = "P18", .Answer = Alternativa.D, .Score = 1.5}
        'Dim p19 = New With {Key .Name = "P19", .Answer = Alternativa.A Or Alternativa.B, .Score = 1.5}
        'Dim p20 = New With {Key .Name = "P20", .Answer = Alternativa.B, .Score = 1.5}
        'Dim p21 = New With {Key .Name = "P21", .Answer = Alternativa.C, .Score = 1.5}
        'Dim p22 = New With {Key .Name = "P22", .Answer = Alternativa.NoAnswer, .Score = 1.5}


        'Dim pdata = New ArrayList({p1, p2, p3, p4, p5,
        '                           p6, p7, p8, p9, p10,
        '                           p11, p12, p13, p14,
        '                           p15, p16, p17, p18,
        '                           p19, p20, p21, p22})

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
        '            Console.WriteLine()
        '            Console.WriteLine(std.Name)
        '            Console.Write("CONTINUAR... ")
        '            If Not String.IsNullOrEmpty(Console.ReadLine()) Then
        '                Continue For
        '            End If
        '        Case "2"
        '            If stdlst.IndexOf(std) < ind - 1 Then
        '                Continue For
        '            End If
        '            Console.WriteLine()
        '            Console.WriteLine(std.Name)
        '            Console.Write("CONTINUAR... ")
        '            If Not String.IsNullOrEmpty(Console.ReadLine()) Then
        '                Continue For
        '            End If
        '        Case "3"
        '            If stdlst.IndexOf(std) <> ind - 1 Then
        '                Continue For
        '            End If
        '    End Select

        '    Console.WriteLine()

        '    Dim grade As Double = 0
        '    Dim score As Double = 0

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

        '        Console.WriteLine()
        '        Console.WriteLine(std.Name)
        '        Console.WriteLine("NOTA EXAMEN TEORICO: " & grade)
        '        Console.Write("CONFIRMAR EVALUACION (SI/NO): ")
        '    Loop While (Console.ReadLine().Trim().ToUpper() = "NO")

        '    std.Grades.Item(Evaluation.P2) = grade

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








        Return 0
    End Function


End Module
