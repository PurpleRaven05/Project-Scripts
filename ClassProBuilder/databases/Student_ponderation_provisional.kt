package com.jorgebg.classprobuilder.databases

import androidx.room.Entity
import androidx.room.ForeignKey
import androidx.room.ForeignKey.CASCADE
import androidx.room.PrimaryKey

@Entity(foreignKeys = [ForeignKey(
        entity = StudentProvisional::class,
        parentColumns = ["id"],
        childColumns = ["studentId"],onDelete = CASCADE),
    ForeignKey(
            entity = subject_ponderation_provisional::class,
            parentColumns = ["id"],
            childColumns = ["pondId"],onDelete = CASCADE),
    ForeignKey(
        entity = Student_Subject_Provisional::class,
        parentColumns = ["id"],
        childColumns = ["subjectId"],onDelete = CASCADE)
                      ],
        tableName = "student_pond_prov")
data class Student_ponderation_provisional (
        @PrimaryKey(autoGenerate = true)
        val id:Int,
        val name : String,
        val studentId:Int,
        val subjectId:Int,
        val pondId:Int,
        val nota:Float
        )
