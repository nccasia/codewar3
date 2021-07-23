<?php

namespace App\Http\Requests;

use Illuminate\Contracts\Validation\Validator;
use Illuminate\Foundation\Http\FormRequest;
use Illuminate\Http\Exceptions\HttpResponseException;
use Illuminate\Http\JsonResponse;
use Illuminate\Validation\ValidationException;

class OrganizationRequest extends FormRequest
{
    /**
     * Determine if the user is authorized to make this request.
     *
     * @return bool
     */
    public function authorize()
    {
        return true;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array
     */
    public function rules()
    {
        return [
            'name' => 'required',
            'linked_in_url' => 'required',
            'brand_id' => 'required|exists:brands,id',
            'owner_id' => 'required|exists:users,user_id',
//            'status' => 'required'
        ];
    }

    public function messages()
    {
        return [
            'name.required' => 'A name for the organization is required.',
            'linked_in_url.required' => 'A linkedInUrl for the organization is required.',
            'brand_id.exists' => 'Not an existing ID'
        ];

    }

    public function failedValidation(Validator $validator)
    {
        $errors = (new ValidationException($validator))->errors();
        throw new HttpResponseException(response()->json(
            [
                'error' => $errors,
                'status_code' => 422,
            ], JsonResponse::HTTP_UNPROCESSABLE_ENTITY));
    }
}
