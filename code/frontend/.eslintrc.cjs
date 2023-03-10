module.exports = {
  env: {
    browser: true,
    es2021: true
  },
  extends: ['eslint:recommended', 'plugin:react/recommended', 'plugin:@typescript-eslint/recommended'],
  parser: '@typescript-eslint/parser',
  parserOptions: {
    ecmaVersion: 'latest',
    sourceType: 'module'
  },
  plugins: ['react', '@typescript-eslint', 'import', 'prettier'],
  rules: {
    'prettier/prettier': 'error',
    '@typescript-eslint/comma-dangle': 'off',
    '@typescript-eslint/indent': ['error', 2],
    'react/jsx-newline': [
      'error',
      {
        prevent: false
      }
    ],
    'react/react-in-jsx-scope': 'off',
    'import/no-duplicates': 'error',
    'import/extensions': 'off',
    'import/order': [
      'error',
      {
        'newlines-between': 'always',
        groups: ['builtin', 'external', 'internal', 'parent', 'sibling'],
        pathGroups: [
          {
            pattern: '~/**',
            group: 'sibling',
            position: 'before'
          }
        ],
        alphabetize: {
          order: 'asc',
          caseInsensitive: true
        }
      }
    ]
  },
  overrides: [
    {
      files: ['*.stories.tsx'],
      rules: {
        'import/no-extraneous-dependencies': 'off'
      }
    }
  ]
};
