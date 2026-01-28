using Microsoft.Data.SqlClient;
using MvcCoreAdoNet.Models;
using System.Data;

namespace MvcCoreAdoNet.Repositories
{
    public class RepositoryHospital
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryHospital()
        {
            string connectionString = @"Data Source=LOCALHOST\DEVELOPER;Initial Catalog=HOSPITAL;User ID=SA;Password=Admin123;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public async Task<List<Hospital>> GetAllHospitalesAsync()
        {
            string sql = "SELECT * FROM HOSPITAL";
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();
            List<Hospital> hospitales = new List<Hospital>();
            this.reader = await this.com.ExecuteReaderAsync();

            while(await this.reader.ReadAsync())
            {
                Hospital hospital = new Hospital();
                hospital.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                hospital.Nombre = this.reader["NOMBRE"].ToString();
                hospital.Direccion = this.reader["DIRECCION"].ToString();
                hospital.Telefono = this.reader["TELEFONO"].ToString();
                hospital.Camas = int.Parse(this.reader["NUM_CAMA"].ToString());
                hospitales.Add(hospital);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return hospitales;
        }

        public async Task<Hospital> FindHospitalAsync(int idHospital)
        {
            string sql = "SELECT * FROM HOSPITAL WHERE HOSPITAL_COD=@hospitalcod";
            this.com.Parameters.AddWithValue("@hospitalcod", idHospital);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            Hospital hospital = new Hospital();
            await this.reader.ReadAsync();
            hospital.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
            hospital.Nombre = this.reader["NOMBRE"].ToString();
            hospital.Direccion = this.reader["DIRECCION"].ToString();
            hospital.Telefono = this.reader["TELEFONO"].ToString();
            hospital.Camas = int.Parse(this.reader["NUM_CAMA"].ToString());
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
            return hospital;
        }

        public async Task InsertHospitalAsync(int idHospital, string nombre, string direccion, string telefono, int camas)
        {
            string sql = "INSERT INTO HOSPITAL VALUES(@hospitalcod, @nombre, @direccion, @telefono, @camas)";
            this.com.Parameters.AddWithValue("@hospitalcod", idHospital);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@direccion", direccion);
            this.com.Parameters.AddWithValue("@telefono", telefono);
            this.com.Parameters.AddWithValue("@camas", camas);
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task UpdateHospitalAsync(int idHospital, string nombre, string direccion,
            string telefono, int camas)
        {
            string sql = "UPDATE HOSPITAL SET NOMBRE=@nombre, DIRECCION=@direccion, TELEFONO=@telefono," +
                " NUM_CAMA=@camas WHERE HOSPITAL_COD=@hospitalcod";
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@direccion", direccion);
            this.com.Parameters.AddWithValue("@telefono", telefono);
            this.com.Parameters.AddWithValue("@camas", camas);
            this.com.Parameters.AddWithValue("@hospitalcod", idHospital);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task DeleteHospitalAsync(int idHospital)
        {
            string sql = "DELETE FROM HOSPITAL WHERE HOSPITAL_COD = @hospitalcod";
            this.com.Parameters.AddWithValue("@hospitalcod", idHospital);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }


        public async Task<List<Doctor>> GetAllDoctoresAsync()
        {
            string sql = "SELECT * FROM DOCTOR";
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();
            List<Doctor> doctores = new List<Doctor>();
            this.reader = await this.com.ExecuteReaderAsync();
            while(await this.reader.ReadAsync())
            {
                Doctor doctor = new Doctor();
                doctor.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                doctor.DoctorId = int.Parse(this.reader["DOCTOR_NO"].ToString());
                doctor.Apellido = this.reader["APELLIDO"].ToString();
                doctor.Especialidad = this.reader["ESPECIALIDAD"].ToString();
                doctor.Salario = int.Parse(this.reader["SALARIO"].ToString());
                doctores.Add(doctor);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return doctores;
        }

        public async Task<List<Doctor>> GetDoctoresEspecialidadAsync(string especialidad)
        {
            string sql = "SELECT * FROM DOCTOR WHERE ESPECIALIDAD=@especialidad";
            this.com.Parameters.AddWithValue("@especialidad", especialidad);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();
            List<Doctor> doctores = new List<Doctor>();
            this.reader = await this.com.ExecuteReaderAsync();
            while (await this.reader.ReadAsync())
            {
                Doctor doctor = new Doctor();
                doctor.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                doctor.DoctorId = int.Parse(this.reader["DOCTOR_NO"].ToString());
                doctor.Apellido = this.reader["APELLIDO"].ToString();
                doctor.Especialidad = this.reader["ESPECIALIDAD"].ToString();
                doctor.Salario = int.Parse(this.reader["SALARIO"].ToString());
                doctores.Add(doctor);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return doctores;
        }


    }
}
