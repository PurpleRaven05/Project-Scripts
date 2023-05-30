package com.jorgebg.classprobuilder.databases

import androidx.room.Entity
import androidx.room.Ignore
import androidx.room.PrimaryKey

@Entity(tableName = "subject_prov")
data class SubjectProvisional (
    @PrimaryKey(autoGenerate = true)
    val id: Int,
    val nombre:String,
    val partes:Int,


)