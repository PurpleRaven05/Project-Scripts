package com.jorgebg.classprobuilder.student

import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.jorgebg.classprobuilder.R
import com.jorgebg.classprobuilder.databases.StudentProvisional

class recyclerAdapterStudents(val names: List<StudentProvisional>, val view: student_menu) : RecyclerView.Adapter<recyclerAdapterStudents.viewHolder>() {


    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): viewHolder {

        val layoutInflater = LayoutInflater.from(parent.context)
        return viewHolder(layoutInflater.inflate(R.layout.recycler_name, parent, false))

    }

    override fun onBindViewHolder(holder: viewHolder, position: Int) {

        with(names[position]){
            holder.nombre.text = names[position].apellido1 + " " + names[position].apellido2 + ", " + names[position].nombre

            holder.itemView.setOnClickListener{
                view.showDialog(names[position].nombre, names[position].apellido1, names[position].id)
                Log.d("Testeo id 1", (names[position].id).toString() )
            }
        }

    }

    override fun getItemCount(): Int {
        Log.d("Tamaño",names.size.toString())
        return names.size
    }



    class viewHolder(val view: View) :RecyclerView.ViewHolder(view){

        val nombre : TextView = view.findViewById(R.id.name_selected)


    }
}