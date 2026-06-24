using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurarRLS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("GRANT USAGE ON SCHEMA \"order\" TO api_order_user;");
            migrationBuilder.Sql("GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA \"order\" TO api_order_user;");
            migrationBuilder.Sql("GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA \"order\" TO api_order_user;");

            migrationBuilder.Sql("ALTER TABLE \"order\".cliente DISABLE ROW LEVEL SECURITY;");
            migrationBuilder.Sql("ALTER TABLE \"order\".negocio DISABLE ROW LEVEL SECURITY;");
            migrationBuilder.Sql("ALTER TABLE \"order\".cliente ENABLE ROW LEVEL SECURITY;");


            migrationBuilder.Sql(@"
                CREATE POLICY cliente_isolation_policy ON ""order"".cliente
                FOR ALL
                TO api_order_user
                USING (
                    ""NegocioId"" = NULLIF(current_setting('app.current_negocio_id', true), '')::uuid
                )
                WITH CHECK (
                    ""NegocioId"" = NULLIF(current_setting('app.current_negocio_id', true), '')::uuid
                );

                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP POLICY IF EXISTS cliente_isolation_policy;");

            migrationBuilder.Sql(@"ALTER TABLE order.cliente DISABLE ROW LEVEL SECURITY;");

            migrationBuilder.Sql(@"
                REVOKE ALL PRIVILEGES ON ALL TABLES IN SCHEMA order FROM api_order_user;
                REVOKE ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA order FROM api_order_user;
            ");
        }
    }
}
