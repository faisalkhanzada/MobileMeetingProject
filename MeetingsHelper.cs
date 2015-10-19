using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
//using System.Data.SQLite;
using MobileMeetingProject.EsEngine;

namespace NewEsApp.Classes
{
    public class MeetingsHelper
    {
        public static void AddMeetingsDB(ObservableCollection<MobileMeetingProject.EsEngine.AttendeeWrapper> Attendees, string EventId)
        {
            DeleteMeetingsDB(EventId);
            foreach (AttendeeWrapper TempAtt in Attendees)
            {
                foreach (MeetingParticipant ThisMeeting in TempAtt.Meetings)
                {
                    SQLiteConnection sqlCon = new SQLiteConnection(Utilities.GetConnectionString());
                    SQLiteCommand sqlCmd = new SQLiteCommand("INSERT INTO Meetings(MeetingId,EventId,Id,Name,CompanyName,Role,Status,IsWalkin,JobTitle,AllocatedTable,"+
                                                             "AttendeeWantId,StartTime,EndTime,IsPrimary,TableName) VALUES(@MeetingId,@EventId,@Id,@Name,@CompanyName,@Role,"+
                                                             "@Status,@IsWalkin,@JobTitle,@AllocatedTable,@AttendeeWantId,@StartTime,@EndTime,@IsPrimary,@TableName)", sqlCon);

                    sqlCmd.Parameters.AddWithValue("@MeetingId", ThisMeeting.MeetingId);
                    sqlCmd.Parameters.AddWithValue("@EventId", EventId);
                    sqlCmd.Parameters.AddWithValue("@Id", ThisMeeting.Id);
                    sqlCmd.Parameters.AddWithValue("@Name", ThisMeeting.Name);
                    sqlCmd.Parameters.AddWithValue("@CompanyName", ThisMeeting.CompanyName);
                    sqlCmd.Parameters.AddWithValue("@Role", ThisMeeting.Role);
                    sqlCmd.Parameters.AddWithValue("@Status", ThisMeeting.Status);
                    sqlCmd.Parameters.AddWithValue("@IsWalkin", ThisMeeting.IsWalkin);
                    sqlCmd.Parameters.AddWithValue("@JobTitle", ThisMeeting.JobTitle);
                    sqlCmd.Parameters.AddWithValue("@AllocatedTable", ThisMeeting.AllocatedTable);
                    sqlCmd.Parameters.AddWithValue("@AttendeeWantId", TempAtt.ThisAttendee.Id);
                    sqlCmd.Parameters.AddWithValue("@StartTime", ThisMeeting.MeetingSlotStartTime);
                    sqlCmd.Parameters.AddWithValue("@EndTime", ThisMeeting.MeetingSlotEndTime);
                    sqlCmd.Parameters.AddWithValue("@IsPrimary", ThisMeeting.IsPrimary);
                    sqlCmd.Parameters.AddWithValue("@TableName", ThisMeeting.TableName);

                    try
                    {
                        sqlCon.Open();
                        int RowsAffected = sqlCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        string Message = ex.Message;
                    }
                    finally
                    {
                        sqlCon.Close();
                    }
                }
            }
        }

        private static void DeleteMeetingsDB(string EventId)
        {
            SQLiteConnection sqlCon = new SQLiteConnection(Utilities.GetConnectionString());
            SQLiteCommand sqlCmd = new SQLiteCommand("DELETE FROM Meetings WHERE EventId = @EventId", sqlCon);

            sqlCmd.Parameters.AddWithValue("@EventId", EventId);

            try
            {
                sqlCon.Open();
                int RowsAffected = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public static ObservableCollection<MeetingParticipant> GetMeetings(string EventId, string AttendeeWantId)
        {
            ObservableCollection<MeetingParticipant> Meetings = new ObservableCollection<MeetingParticipant>();

            SQLiteConnection sqlCon = new SQLiteConnection(Utilities.GetConnectionString());
            SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM Meetings WHERE EventId = @EventId AND AttendeeWantId = @AttendeeWantId", sqlCon);

            sqlCmd.Parameters.AddWithValue("@EventId", EventId);
            sqlCmd.Parameters.AddWithValue("@AttendeeWantId", AttendeeWantId);

            try
            {
                sqlCon.Open();

                SQLiteDataReader reader = sqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MeetingParticipant ThisMeeting = new MeetingParticipant();

                        ThisMeeting.MeetingId = reader.GetInt32(0);
                        ThisMeeting.Id = reader.GetString(2);
                        ThisMeeting.Name = reader[3] != DBNull.Value ? reader.GetString(3) : string.Empty;
                        ThisMeeting.CompanyName = reader[4] != DBNull.Value ? reader.GetString(4) : string.Empty;
                        ThisMeeting.Role = reader[5] != DBNull.Value ? reader.GetString(5) : string.Empty;
                        ThisMeeting.Status = reader[6] != DBNull.Value ? reader.GetString(6) : string.Empty;
                        ThisMeeting.IsWalkin = reader.GetBoolean(7);
                        ThisMeeting.JobTitle = reader[8] != DBNull.Value ? reader.GetString(8) : string.Empty;
                        ThisMeeting.AllocatedTable = reader[9] != DBNull.Value ? reader.GetString(9) : string.Empty;
                        ThisMeeting.MeetingSlotStartTime = reader.GetDateTime(11);
                        ThisMeeting.MeetingSlotEndTime = reader.GetDateTime(12);
                        ThisMeeting.IsPrimary = reader.GetBoolean(13);
                        ThisMeeting.TableName = reader.GetString(14);

                        Meetings.Add(ThisMeeting);
                    }
                }
            }
            catch (Exception ex)
            {
                string Meesage = ex.Message;
            }
            finally
            {
                sqlCon.Close();
            }

            return Meetings;
        }
    }
}
