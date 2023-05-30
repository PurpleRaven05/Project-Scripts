package com.jorgebg.classprobuilder.databases

import androidx.room.Entity
import androidx.room.ForeignKey
import androidx.room.ForeignKey.CASCADE
import androidx.room.PrimaryKey

@Entity(tableName = "subject_ponderations", foreignKeys = [ForeignKey(
    entity = SubjectProvisional::class,
    parentColumns = ["id"],
    childColumns = ["subjectId"], onDelete = CASCADE)])
data class subject_ponderation_provisional (
    @PrimaryKey(autoGenerate = true)
    val id: Int,
    val subjectId:Int,
    val name:String,
    val weight:Float
)
