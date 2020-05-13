<?php
$id = $_POST['id'];
DB::select("UPDATE inventory SET item_status='game' WHERE id=$id");

// if ($conn->query($sql) === TRUE) {
//     echo "New record updated successfully";
// } else {
//     echo "Error: " . $sql . "<br>" . $conn->error;
// }

// $conn->close();
?>