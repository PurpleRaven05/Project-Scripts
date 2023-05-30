package com.jorgebg.classprobuilder.Model

open class SingletoneHolder<out T, in A>(private val constructor: (A) -> T) {
    @Volatile
    private var instance: T? =null
    fun getInstance(arg: A): T =
        instance ?: synchronized(this) {
            instance ?:constructor(arg).also { instance = it}
        }
}