Imports System.IO
Imports System.Text
Imports System.Windows.Threading
Module LC_RunProgram
    Dim program_dir As String
    Public CompileError As String
    Public ProgramOutput As String
    Public WithEvents compileWorker As New System.ComponentModel.BackgroundWorker
    Public WithEvents testWorker As New System.ComponentModel.BackgroundWorker

    Public c_e As LCLib.CustomControls.ConsoleEmulator
    Public Sub Compile() Handles compileWorker.DoWork
        Select Case Language_Used
            Case "Java"
                CompileError = JavaCompile()
            Case "C++"
                CompileError = CppCompile()
            Case "HTML"
                CompileError = Nothing
                Run()
            Case "JavaScript"
                CompileError = Nothing
                Run()
            Case "PHP"
                CompileError = Nothing
                Run()
        End Select
    End Sub
    Public Sub TestRun() Handles testWorker.DoWork
        Select Case Language_Used
            Case "Java"
                CompileError = JavaCompile()
            Case "C++"
                CompileError = CppCompile()
            Case "HTML"
                CompileError = Nothing
                Run()
            Case "JavaScript"
                CompileError = Nothing
                Run()
            Case "PHP"
                CompileError = Nothing
                Run()
        End Select
    End Sub

    Public Sub Run()
        Select Case Language_Used
            Case "Java"
                JavaOutput()
            Case "C++"
                CppOutput()
            Case "HTML"
                ProgramOutput = HTMLOutput()
            Case "JavaScript"
                ProgramOutput = JavaScriptOutput()
            Case "PHP"
                ProgramOutput = PHPOutput()
        End Select
    End Sub
    Public Sub compileWorker_RunWorkerCompleted() Handles compileWorker.RunWorkerCompleted
        LC_Window.LC_LessonScreen.LCodeScreen.Compile_Finished()
        c_e = LC_Window.LC_CodeOutputScreen.CConsoleScreen.console_emulator
        If CompileError = Nothing Then
            Run()
            Return
        End If
        c_e.Text = CompileError
    End Sub
    Public Sub testWorker_RunWorkerCompleted() Handles testWorker.RunWorkerCompleted
        LC_Window.LC_TestScreen.CodingScreen.Compiled_Finished()
        c_e = LC_Window.LC_TestScreen.CodingScreen.test_console
        If CompileError = Nothing Then
            Run()
            Return
        End If
        c_e.Text = CompileError
        LC_Window.LC_TestScreen.CodingScreen.Compile_error()
    End Sub
    Dim procInf As New ProcessStartInfo
    Dim proc As New Process
    Dim class_name As String
    '------------------------------------------------------------------------------------------
    '----------------------------------JAVA----------------------------------------------------
    '------------------------------------------------------------------------------------------
    Dim java_code As String
    Public Sub JavaOutput()
        If File.Exists(program_dir & "\" & class_name & ".class") Then
            c_e.StartProcess("java", class_name, program_dir)
        End If
    End Sub
    Public Function JavaCompile()
        program_dir = users_dir & "\" & current_user.Username & "\Programs\Java"

        If Not Directory.Exists(program_dir) Then
            Directory.CreateDirectory(program_dir)
        End If
        class_name = ClassName_Edited
        java_code = Code_Edited
        File.WriteAllText(program_dir & "\" & class_name & ".java", java_code)

        procInf = New ProcessStartInfo
        proc = New Process
        procInf.WorkingDirectory = program_dir
        procInf.FileName = "javac"
        procInf.Arguments = class_name & ".java"
        procInf.UseShellExecute = False
        procInf.RedirectStandardError = True
        procInf.CreateNoWindow = True
        proc.StartInfo = procInf
        proc.Start()
        Dim err As String = proc.StandardError.ReadToEnd
        proc.WaitForExit()
        procInf = New ProcessStartInfo
        proc = New Process
        If err <> Nothing Then
            Return err
        End If
        Return Nothing
    End Function

    '------------------------------------------------------------------------------------------
    '----------------------------------C++-----------------------------------------------------
    '------------------------------------------------------------------------------------------
    Dim cpp_code As String
    Public Sub CppOutput()
        If File.Exists(program_dir & "\" & class_name & ".exe") Then
            c_e.StartProcess(program_dir & "\" & class_name & ".exe", "", "")
        End If
    End Sub
    Public Function CppCompile() As String
        program_dir = users_dir & "\" & current_user.Username & "\Programs\C++"

        If Not Directory.Exists(program_dir) Then
            Directory.CreateDirectory(program_dir)
        End If
        class_name = ClassName_Edited
        cpp_code = Code_Edited
        File.WriteAllText(program_dir & "\" & class_name & ".cpp", cpp_code)
        procInf = New ProcessStartInfo
        proc = New Process
        procInf.WorkingDirectory = program_dir
        procInf.FileName = "g++"
        procInf.Arguments = class_name & ".cpp -o " & class_name & ".exe"
        procInf.UseShellExecute = False
        procInf.RedirectStandardError = True
        procInf.CreateNoWindow = True
        proc.StartInfo = procInf
        proc.Start()
        Dim err As String = proc.StandardError.ReadToEnd
        proc.WaitForExit()
        procInf = New ProcessStartInfo
        proc = New Process
        If err <> Nothing Then
            Return err
        End If
        Return Nothing
    End Function
    '------------------------------------------------------------------------------------------
    '----------------------------------HTML----------------------------------------------------
    '------------------------------------------------------------------------------------------
    Dim html_code As String
    Public Function HTMLOutput() As String
        program_dir = users_dir & "\" & current_user.Username & "\Programs\HTML"
        If Not Directory.Exists(program_dir) Then
            Directory.CreateDirectory(program_dir)
        End If
        class_name = ClassName_Edited
        html_code = Code_Edited
        File.WriteAllText(program_dir & "\" & class_name & ".html", html_code)
        Dim output As String = html_code
        Return output
    End Function
    '------------------------------------------------------------------------------------------
    '-------------------------------JavaScript-------------------------------------------------
    '------------------------------------------------------------------------------------------
    Dim js_code As String
    Public Function JavaScriptOutput() As String
        program_dir = users_dir & "\" & current_user.Username & "\Programs\JavaScript"
        If Not Directory.Exists(program_dir) Then
            Directory.CreateDirectory(program_dir)
        End If
        class_name = ClassName_Edited
        js_code = Code_Edited
        File.WriteAllText(program_dir & "\" & class_name & ".html", js_code)
        Dim output As String = js_code
        Return output
    End Function
    '------------------------------------------------------------------------------------------
    '---------------------------------PHP------------------------------------------------------
    '------------------------------------------------------------------------------------------
    Dim php_code As String
    Public Function PHPOutput() As String

        program_dir = users_dir & "\" & current_user.Username & "\Programs\PHP"
        If Not Directory.Exists(program_dir) Then
            Directory.CreateDirectory(program_dir)
        End If
        class_name = ClassName_Edited
        php_code = Code_Edited
        File.WriteAllText(program_dir & "\" & class_name & ".php", php_code)
        procInf = New ProcessStartInfo
        proc = New Process
        procInf.WorkingDirectory = program_dir
        procInf.FileName = "php"
        procInf.Arguments = class_name & ".php"
        procInf.UseShellExecute = False
        procInf.RedirectStandardOutput = True
        procInf.RedirectStandardError = True
        procInf.CreateNoWindow = True
        proc.StartInfo = procInf
        proc.Start()
        Dim output As String = proc.StandardOutput.ReadToEnd
        Dim err As String = proc.StandardError.ReadToEnd
        proc.WaitForExit()
        If err <> Nothing Then
            Return err
        End If
        Return output
    End Function
End Module