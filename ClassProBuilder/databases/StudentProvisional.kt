package com.jorgebg.classprobuilder.databases

import androidx.room.Entity
import androidx.room.Ignore
import androidx.room.PrimaryKey

@Entity(tableName = "student_prov")
data class StudentProvisional (
    @PrimaryKey(autoGenerate = true)
    val id: Int,
    val nombre:String,
    val apellido1:String,
    val apellido2:String?,

)