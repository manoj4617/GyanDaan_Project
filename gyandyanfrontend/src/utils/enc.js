
export  function encrypt(str) {
  //add 1 to every character in the string
  var encrypted = '';
  for (var i = 0; i < str.length; i++) {
    var char = str[i];
    var code = char.charCodeAt(0);
    code++;
    encrypted += String.fromCharCode(code);
  }
  return encrypted;
}

export  function decrypt(str) {
    //subtract 1 from every character in the string
    var decrypted = '';
    for (var i = 0; i < str.length; i++) {
      var char = str[i];
      var code = char.charCodeAt(0);
      code--;
      decrypted += String.fromCharCode(code);
    }
    return decrypted;
}


