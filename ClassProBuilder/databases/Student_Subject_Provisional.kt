package com.jorgebg.classprobuilder.databases

import androidx.room.Entity
import androidx.room.ForeignKey
import androidx.room.ForeignKey.CASCADE
import androidx.room.Ignore
import androidx.room.PrimaryKey

@Entity(foreignKeys = [ForeignKey(
    entity = StudentProvisional::class,
    parentColumns = ["id"],
    childColumns = ["studentId"],onDelete = CASCADE),
    ForeignKey(
        entity = SubjectProvisional::class,
        parentColumns = ["id"],
        childColumns = ["subjectId"],onDelete = CASCADE)],
        tableName = "student_subject_prov")
data class Student_Subject_Provisional (
        @PrimaryKey(autoGenerate = true)
        val id:Int,
        val studentId:Int,
        val subjectId:Int,
        val trimestre:Int,
        val nombre:String,
        val Media:Float?

)