package com.jorgebg.classprobuilder.databases

import android.content.Context
import androidx.room.Database
import androidx.room.Room
import androidx.room.RoomDatabase
import com.jorgebg.classprobuilder.Model.SingletoneHolder

@Database(entities= [StudentProvisional::class,
Student_Subject_Provisional::class, SubjectProvisional::class, subject_ponderation_provisional::class, Student_ponderation_provisional::class],
version = 1, exportSchema = false)
abstract class provisionalDatabase:RoomDatabase() {
    abstract fun getDao(): Provisional_Dao

    companion object {
        @Volatile
        private var INSTANCE: provisionalDatabase? = null

        //Para que solo haya una instancia
        fun getDatabase(context: Context): provisionalDatabase {
            val tempInstance = INSTANCE
            if (tempInstance != null) {
                return tempInstance
            }
            synchronized(this) {
                val Instance = Room.databaseBuilder(
                    context.applicationContext,
                    provisionalDatabase::class.java,
                    "countryDatabase"
                ).build()
                INSTANCE = Instance
                return Instance
            }
        }
    }
}