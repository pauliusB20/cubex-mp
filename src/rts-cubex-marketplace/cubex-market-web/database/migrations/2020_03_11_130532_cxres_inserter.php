<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CxresInserter extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        DB::unprepared("
        CREATE TRIGGER starting_res_inserter AFTER INSERT ON `users` FOR EACH ROW
          BEGIN
                INSERT INTO `transactions_resources` (`from_user_id`, `to_user_id`, `res_type`, `amount`, `type_of_transaction`)
                VALUES ((SELECT min(`id`) FROM `users` WHERE `role` LIKE 'admin'), (SELECT max(`id`) FROM `users` where `role` LIKE 'player'), 'credits', 100, 'from_admin_to_web_user');
                INSERT INTO `transactions_resources` (`from_user_id`, `to_user_id`, `res_type`, `amount`, `type_of_transaction`)
                VALUES ((SELECT min(`id`) FROM `users` WHERE `role` LIKE 'admin'), (SELECT max(`id`) FROM `users` where `role` LIKE 'player'), 'energon', 150, 'from_admin_to_web_user');
           END
        ");

        // DB::unprepared("
        // CREATE TRIGGER starting_res_inserter AFTER INSERT ON `users` FOR EACH ROW
        //   BEGIN
        //     IF NEW.role LIKE 'player' THEN
        //         INSERT INTO `transactions_resources` (`from_user_id`, `to_user_id`, `amount`, `type`)
        //         VALUES ((SELECT min(`id`) FROM `users` WHERE `role` LIKE 'admin'), (SELECT max(`id`) FROM `users`), 100, 'credits');
        //         INSERT INTO `transactions_resources` (`from_user_id`, `to_user_id`, `amount`, `type`)
        //         VALUES ((SELECT min(`id`) FROM `users` WHERE `role` LIKE 'admin'), (SELECT max(`id`) FROM `users`), 150, 'energon');
        //     ENDIF
        //    END
        // ");
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        DB::unprepared('DROP TRIGGER `starting_res_inserter`');
    }
}
