using CheckoutAPI.Models;
using System;
using System.Data;

namespace CheckoutAPI.DataStorage
{
    public class PostgresStorage : IPaymentStorage
    {
        private IDbConnection dbConnection;
        private string tableName = "PaymentStore";

        public PostgresStorage(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public PaymentStorageObject Retrieve(int id)
        {
            string commandText = $@"Select id as id, maskedCardNumber as maskedCardNumber, hashedCardNumber as hashedCardNumber, expiryDate as expiryDate, amount as amount, currency as currency, transactionTime as transactionTime, approved as approved
                                    from {tableName} 
                                    where id='{id}'";

            PaymentStorageObject result = null;

            OpenConnection(dbConnection);
            using (IDbCommand dbCommand = dbConnection.CreateCommand()) {
                try {
                    dbCommand.CommandText = commandText;
                    using (var databaseResult = dbCommand.ExecuteReader())
                    {
                        while (databaseResult.Read()) {
                            var maskedCardNumber = databaseResult.GetString(1);
                            var hashedCardNumber = databaseResult.GetInt32(2);
                            var expiryDate = databaseResult.GetDateTime(3);
                            var amount = databaseResult.GetDecimal(4);
                            var currency = databaseResult.GetString(5);
                            var transactionTime = databaseResult.GetDateTime(6);
                            var approved = databaseResult.GetBoolean(7);

                            result = new PaymentStorageObject(maskedCardNumber, hashedCardNumber, expiryDate, amount, currency, transactionTime, approved);
                        }
                    }
                } finally {
                    CloseConnection(dbConnection);
                }
            }

            return result;
        }

        public void Store(int id, PaymentStorageObject paymentDetails)
        {
            string commandText = $@"INSERT INTO {tableName} (id, maskedCardNumber, hashedCardNumber, expiryDate, amount, currency, transactionTime, approved)
						VALUES ('{id}', '{paymentDetails.maskedCardNumber}', '{paymentDetails.hashedCardNumber}', '{(NpgsqlTypes.NpgsqlDateTime)paymentDetails.expiryDate}', 
                        '{paymentDetails.amount}', '{paymentDetails.currency}', '{(NpgsqlTypes.NpgsqlDateTime)paymentDetails.transactionTime}', '{paymentDetails.approved}')";

            OpenConnection(dbConnection);
            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                try {
                    dbCommand.CommandText = commandText;
                    dbCommand.ExecuteNonQuery();
                } finally {
                    CloseConnection(dbConnection);
                }
            }
        }

        private void CloseConnection(IDbConnection dbConnection)
        {
            if (dbConnection.State != ConnectionState.Closed)
            {
                dbConnection.Close();
            }
        }

        private void OpenConnection(IDbConnection dbConnection)
        {
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
        }
    }
}
