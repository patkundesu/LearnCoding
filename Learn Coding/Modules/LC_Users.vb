Imports LCLib
Imports LCLib.Functions
Imports System.IO
Imports System.Data.SQLite
Imports System.ComponentModel
Module LC_Users
    Public users_dir As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Learn Coding Users"
    Public users_list As List(Of lc_user)
    Public current_user As lc_user

    Public Sub LoadUsers()
        If Not Directory.Exists(users_dir) Then
            Directory.CreateDirectory(users_dir)
        End If
        Dim user_dirs() As String = Directory.GetDirectories(users_dir)
        users_list = New List(Of lc_user)
        For Each user_dir As String In user_dirs
            Dim username As String = user_dir.Remove(0, users_dir.Length + 1)
            Dim user_db As String = user_dir & "\" & username & ".db"

            Dim user As New lc_user

            Using cn As New SQLiteConnection("Data Source=" & user_db & ";Version=3;")
                cn.Open()
                Using cmd As New SQLiteCommand("SELECT * FROM main_info", cn)
                    Using rs As SQLiteDataReader = cmd.ExecuteReader()
                        While rs.Read
                            user.Username = rs("username").ToString.Decrypt
                            user.Password = rs("password").ToString.Decrypt
                            user.Created = rs("date_created")
                            If rs("current_unit") = 0 Then
                                user.Progress.Current_Unit = Fundamentals
                            ElseIf rs("current_unit") = 1 Then
                                user.Progress.Current_Unit = Java
                            ElseIf rs("current_unit") = 2 Then
                                user.Progress.Current_Unit = CPP
                            ElseIf rs("current_unit") = 3 Then
                                user.Progress.Current_Unit = HTML
                            ElseIf rs("current_unit") = 4 Then
                                user.Progress.Current_Unit = Javascript
                            ElseIf rs("current_unit") = 5 Then
                                user.Progress.Current_Unit = PHP
                            End If
                            user.Progress.Current_Lesson = rs("current_lesson") - 1
                            user.Progress.Current_Page = rs("current_page") - 1
                        End While
                    End Using
                End Using
                Using cmd As New SQLiteCommand("SELECT * FROM lesson_stats", cn)
                    Using rs As SQLiteDataReader = cmd.ExecuteReader
                        While rs.Read
                            If rs("language") = "Fundamentals" Then
                                user.FundamentalStats.CurrentLesson = rs("current_lesson") - 1
                                user.FundamentalStats.CurrentPage = rs("current_page") - 1
                                user.FundamentalStats.Percentage = rs("percentage")
                                user.FundamentalStats.Finished = rs("isfinished")
                            ElseIf rs("language") = "Java" Then
                                user.JavaStats.CurrentLesson = rs("current_lesson") - 1
                                user.JavaStats.CurrentPage = rs("current_page") - 1
                                user.JavaStats.Percentage = rs("percentage")
                                user.JavaStats.Finished = rs("isfinished")
                            ElseIf rs("language") = "C++" Then
                                user.CppStats.CurrentLesson = rs("current_lesson") - 1
                                user.CppStats.CurrentPage = rs("current_page") - 1
                                user.CppStats.Percentage = rs("percentage")
                                user.CppStats.Finished = rs("isfinished")
                            ElseIf rs("language") = "HTML" Then
                                user.HTMLStats.CurrentLesson = rs("current_lesson") - 1
                                user.HTMLStats.CurrentPage = rs("current_page") - 1
                                user.HTMLStats.Percentage = rs("percentage")
                                user.HTMLStats.Finished = rs("isfinished")
                            ElseIf rs("language") = "JavaScript" Then
                                user.JavaScriptStats.CurrentLesson = rs("current_lesson") - 1
                                user.JavaScriptStats.CurrentPage = rs("current_page") - 1
                                user.JavaScriptStats.Percentage = rs("percentage")
                                user.JavaScriptStats.Finished = rs("isfinished")
                            ElseIf rs("language") = "PHP" Then
                                user.PHPStats.CurrentLesson = rs("current_lesson") - 1
                                user.PHPStats.CurrentPage = rs("current_page") - 1
                                user.PHPStats.Percentage = rs("percentage")
                                user.PHPStats.Finished = rs("isfinished")
                            End If
                        End While
                    End Using
                End Using
                Using cmd As New SQLiteCommand("SELECT * FROM fundamentals_test", cn)
                    Using rs As SQLiteDataReader = cmd.ExecuteReader
                        While rs.Read
                            Dim a_stats As New test_stats
                            a_stats.TestNumber = rs("test_number")
                            a_stats.Score = rs("score")
                            a_stats.Percentage = rs("percentage")
                            user.FundamentalStats.Tests.Add(a_stats)
                        End While
                    End Using
                End Using
                Using cmd As New SQLiteCommand("SELECT * FROM java_test", cn)
                    Using rs As SQLiteDataReader = cmd.ExecuteReader
                        While rs.Read
                            Dim a_stats As New test_stats
                            a_stats.TestNumber = rs("test_number")
                            a_stats.Score = rs("score")
                            a_stats.Percentage = rs("percentage")
                            user.JavaStats.Tests.Add(a_stats)
                        End While
                    End Using
                End Using
                Using cmd As New SQLiteCommand("SELECT * FROM cpp_test", cn)
                    Using rs As SQLiteDataReader = cmd.ExecuteReader
                        While rs.Read
                            Dim a_stats As New test_stats
                            a_stats.TestNumber = rs("test_number")
                            a_stats.Score = rs("score")
                            a_stats.Percentage = rs("percentage")
                            user.CppStats.Tests.Add(a_stats)
                        End While
                    End Using
                End Using
                Using cmd As New SQLiteCommand("SELECT * FROM html_test", cn)
                    Using rs As SQLiteDataReader = cmd.ExecuteReader
                        While rs.Read
                            Dim a_stats As New test_stats
                            a_stats.TestNumber = rs("test_number")
                            a_stats.Score = rs("score")
                            a_stats.Percentage = rs("percentage")
                            user.CppStats.Tests.Add(a_stats)
                        End While
                    End Using
                End Using
                Using cmd As New SQLiteCommand("SELECT * FROM javascript_test", cn)
                    Using rs As SQLiteDataReader = cmd.ExecuteReader
                        While rs.Read
                            Dim a_stats As New test_stats
                            a_stats.TestNumber = rs("test_number")
                            a_stats.Score = rs("score")
                            a_stats.Percentage = rs("percentage")
                            user.JavaScriptStats.Tests.Add(a_stats)
                        End While
                    End Using
                End Using
                Using cmd As New SQLiteCommand("SELECT * FROM php_test", cn)
                    Using rs As SQLiteDataReader = cmd.ExecuteReader
                        While rs.Read
                            Dim a_stats As New test_stats
                            a_stats.TestNumber = rs("test_number")
                            a_stats.Score = rs("score")
                            a_stats.Percentage = rs("percentage")
                            user.PHPStats.Tests.Add(a_stats)
                        End While
                    End Using
                End Using
            End Using
            users_list.Add(user)
        Next
    End Sub
    Public Sub CreateNew(userN As String, passW As String)

        Dim new_user_dir As String = users_dir & "\" & userN
        Dim new_user_db As String = new_user_dir & "\" & userN & ".db"
        Directory.CreateDirectory(new_user_dir)

        SQLiteConnection.CreateFile(new_user_db)
        Using cn As New SQLiteConnection("Data Source=" & new_user_db & ";Version=3;")
            cn.Open()
            Dim dbSql As New System.Text.StringBuilder

            dbSql.AppendLine("CREATE TABLE main_info (username VARCHAR(50), password VARCHAR(50), date_created TEXT, current_unit INTEGER, current_lesson INTEGER, current_page INTEGER);")
            dbSql.AppendLine("INSERT INTO main_info VALUES ('" & userN.Encrypt & "','" & passW.Encrypt & "','" & Date.Now.ToShortDateString & "', 0, 1, 1);")

            dbSql.AppendLine("CREATE TABLE fundamentals_test (test_number INTEGER, score INTEGER, percentage REAL);")
            dbSql.AppendLine("CREATE TABLE java_test (test_number INTEGER, score INTEGER, percentage REAL);")
            dbSql.AppendLine("CREATE TABLE cpp_test (test_number INTEGER, score INTEGER, percentage REAL);")
            dbSql.AppendLine("CREATE TABLE html_test (test_number INTEGER, score INTEGER, percentage REAL);")
            dbSql.AppendLine("CREATE TABLE javascript_test (test_number INTEGER, score INTEGER, percentage REAL);")
            dbSql.AppendLine("CREATE TABLE php_test (test_number INTEGER, score INTEGER, percentage REAL);")

            dbSql.AppendLine("CREATE TABLE lesson_stats (language TEXT, current_lesson INTEGER, current_page INTEGER, percentage REAL, isfinished INTEGER);")
            dbSql.AppendLine("INSERT INTO lesson_stats VALUES ('Fundamentals',1, 1, 0.0, 0);")
            dbSql.AppendLine("INSERT INTO lesson_stats VALUES ('Java',1, 1, 0.0, 0);")
            dbSql.AppendLine("INSERT INTO lesson_stats VALUES ('C++',1, 1, 0.0, 0);")
            dbSql.AppendLine("INSERT INTO lesson_stats VALUES ('HTML',1, 1, 0.0, 0);")
            dbSql.AppendLine("INSERT INTO lesson_stats VALUES ('JavaScript',1, 1, 0.0, 0);")
            dbSql.AppendLine("INSERT INTO lesson_stats VALUES ('PHP',1, 1, 0.0, 0);")
            Using cmd As New SQLiteCommand(dbSql.ToString(), cn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
        LoadUsers()
    End Sub
    Public Sub UpdateProgress()
        Using cn As New SQLiteConnection("Data Source=" & users_dir & "\" & current_user.Username & "\" & current_user.Username & ".db")
            cn.Open()
            Dim dbSql As New System.Text.StringBuilder
            dbSql.AppendLine("UPDATE main_info SET current_unit = " & (current_user.Progress.Current_Unit.Number) & " WHERE username = '" & current_user.Username.Encrypt & "';")
            dbSql.AppendLine("UPDATE main_info SET current_lesson = " & (current_user.Progress.Current_Lesson + 1) & " WHERE username = '" & current_user.Username.Encrypt & "';")
            dbSql.AppendLine("UPDATE main_info SET current_page = " & (current_user.Progress.Current_Page + 1) & " WHERE username = '" & current_user.Username.Encrypt & "';")

            dbSql.AppendLine("UPDATE lesson_stats SET current_lesson = " & (current_user.Progress.Current_Lesson + 1) & " WHERE language = '" & current_unit.Language & "';")
            dbSql.AppendLine("UPDATE lesson_stats SET current_page = " & (current_user.Progress.Current_Page + 1) & " WHERE language = '" & current_unit.Language & "';")

            Using cmd As New SQLiteCommand(dbSql.ToString, cn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    ''' <summary>
    ''' Checks if the User exists in the users list
    ''' </summary>
    ''' <param name="userN"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AccountExist(ByVal userN As String) As Boolean
        For Each user In users_list
            If user.Username = userN Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Function Validation(username As String, password As String)
        If current_user.Username = username And current_user.Password = password Then
            Return True
        End If
        Return False
    End Function
    Public Sub SignIn()
        Dim h_screen = LC_Window.LC_UserHomeScreen
        current_unit = current_user.Progress.Current_Unit
        current_lesson = current_user.Progress.Current_Lesson
        current_page = current_user.Progress.Current_Page

        If Equals(current_unit, Fundamentals) _
            And current_lesson = 0 _
            And current_page = 0 Then
            LC_Window.LC_UserHomeScreen.btn_lesson.Content = "Start"
        Else
            LC_Window.LC_UserHomeScreen.btn_lesson.Content = "Continue"
        End If

        h_screen.UpdateProgressInfo()

        h_screen.txtb_user.Text = current_user.Username

        LC_Window.btn_user.Content = current_user.Username
        LC_Window.btn_user.ShowObject()
    End Sub
    Public Sub SignOut()
        Dim h_screen = LC_Window.LC_UserHomeScreen
        current_unit = New unit
        current_lesson = 0
        current_page = 0

        current_user = New lc_user

        LC_Window.btn_user.Content = "username"
        LC_Window.btn_user.HideObject()
    End Sub
    Public Sub DeleteUser()
        System.IO.Directory.Delete(users_dir & "\" & current_user.Username, True)
        LoadUsers()
        If users_list.Count > 0 Then
            LC_Window.LC_UsersListPanel.UpdateList()
        Else
            LC_Window.LC_UsersListPanel.NextObject(LC_Window.LC_UserCreatePanel)
            LC_Window.LC_HomeScreen.btn_sign.Content = "Create User"
        End If
    End Sub
    Public Sub UnitFinished()
        Using cn As New SQLiteConnection("Data Source=" & users_dir & "\" & current_user.Username & "\" & current_user.Username & ".db")
            cn.Open()
            Dim dbSql As New System.Text.StringBuilder
            dbSql.AppendLine("UPDATE lesson_stats SET isfinished = 1 WHERE language = '" & current_unit.Language & "';")
            Using cmd As New SQLiteCommand(dbSql.ToString, cn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Module
